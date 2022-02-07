using System.Linq;
using AutoMapper;
using PokeDex.Api.Domain;
using PokeDex.Api.Responses;

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