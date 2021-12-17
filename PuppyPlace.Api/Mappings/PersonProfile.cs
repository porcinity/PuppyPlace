using AutoMapper;
using PuppyPlace.Api.Dtos;
using PuppyPlace.Domain;

namespace PuppyPlace.Api.Mappings;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, GetPersonDto>()
            .ForMember(p => p.Age, 
                opt => opt.MapFrom(src => src.Age.Value) );
        CreateMap<Dog, PersonDogsDto>();
        CreateMap<IEnumerable<GetPersonDto>, GetPersonsDto>();
    }
}