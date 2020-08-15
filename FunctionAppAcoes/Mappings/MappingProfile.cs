using AutoMapper;
using FunctionAppAcoes.Models;
using FunctionAppAcoes.Documents;

namespace FunctionAppAcoes.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Acao, AcaoDocument>()
                .ForMember(dest => dest.Sigla, m => m.MapFrom(a => a.Codigo))
                .ForMember(dest => dest.Valor, m => m.MapFrom(a => a.Valor))
                .ForMember(dest => dest.Data, m => m.MapFrom(a => a.UltimaAtualizacao))
                .ForMember(dest => dest.HistLancamento,
                    m => m.MapFrom(
                        a => a.Codigo + "-" + a.UltimaAtualizacao.ToString("yyyyMMdd-HHmmss")));
                
            CreateMap<AcaoDocument, Acao>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(d => d.Sigla))
                .ForMember(dest => dest.Valor, m => m.MapFrom(d => d.Valor))
                .ForMember(dest => dest.UltimaAtualizacao, m => m.MapFrom(d => d.Data));
        }
    }
}