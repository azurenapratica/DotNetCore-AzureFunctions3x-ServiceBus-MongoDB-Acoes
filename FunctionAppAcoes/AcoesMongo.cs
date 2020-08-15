using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FunctionAppAcoes.Data;

namespace FunctionAppAcoes
{
    public class AcoesMongo
    {
        private readonly AcoesRepository _repository;

        public AcoesMongo(AcoesRepository repository)
        {
            _repository = repository;
        }

        [FunctionName("AcoesMongo")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var listaAcoes = _repository.ListAll();
            log.LogInformation(
                $"AcoesMongo HTTP trigger - número atual de lançamentos: {listaAcoes.Count}");
            return new OkObjectResult(listaAcoes);
        }
    }
}