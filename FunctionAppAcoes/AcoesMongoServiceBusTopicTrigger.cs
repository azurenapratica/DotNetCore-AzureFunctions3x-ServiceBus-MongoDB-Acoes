using System;
using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using FunctionAppAcoes.Models;
using FunctionAppAcoes.Validators;
using FunctionAppAcoes.Data;

namespace FunctionAppAcoes
{
    public class AcoesMongoServiceBusTopicTrigger
    {
        private readonly AcoesRepository _repository;

        public AcoesMongoServiceBusTopicTrigger(
            AcoesRepository repository)
        {
            _repository = repository;
        }

        [FunctionName("AcoesMongoServiceBusTopicTrigger")]
        public void Run([ServiceBusTrigger("topic-acoes", "mongo", Connection = "AzureServiceBus")] string mySbMsg, ILogger log)
        {
            log.LogInformation($"AcoesMongoServiceBusTopicTrigger - Dados: {mySbMsg}");

            Acao acao = null;
            try
            {
                acao = JsonSerializer.Deserialize<Acao>(mySbMsg,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                log.LogError("AcoesMongoServiceBusTopicTrigger - Erro durante a deserializacao!");
            }

            if (acao != null)
            {
                var validationResult = new AcaoValidator().Validate(acao);
                if (validationResult.IsValid)
                {
                    acao.UltimaAtualizacao = DateTime.Now;
                    log.LogInformation($"AcoesMongoServiceBusTopicTrigger - Dados pos formatacao: {JsonSerializer.Serialize(acao)}");
                    _repository.Save(acao);
                    log.LogInformation("AcoesMongoServiceBusTopicTrigger - Acao registrada com sucesso!");
                }
                else
                {
                    log.LogError("AcoesMongoServiceBusTopicTrigger - Dados invalidos para a Acao");
                    foreach (var error in validationResult.Errors)
                        log.LogError($"AcoesMongoServiceBusTopicTrigger - {error.ErrorMessage}");
                }
            }
        }
    }
}