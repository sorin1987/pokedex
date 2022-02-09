using AutoMapper;
using PokeDex.Api.Contracts.Responses;
using PokeDex.Api.Domain.Models.Pokemon;
using System.Linq;

namespace PokeDex.Api.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Pokemon, PokemonResponse>()
                .ForMember(x => x.Description,
                    opt => opt.MapFrom(src => src.Descriptions.FirstOrDefault(d => d.Language.Name == "en").Text))
                .ForMember(x=>x.Habitat, opt=>opt.MapFrom(src=>src.Habitat.Name));
        }
    }
}