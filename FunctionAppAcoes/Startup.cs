using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FunctionAppAcoes.Mappings;
using FunctionAppAcoes.Data;

[assembly: FunctionsStartup(typeof(FunctionAppAcoes.Startup))]
namespace FunctionAppAcoes
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddScoped<AcoesRepository>();
        }
    }
}