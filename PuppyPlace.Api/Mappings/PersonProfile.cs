using AutoMapper;
using PuppyPlace.Api.Dtos;
using PuppyPlace.Domain;

namespace PuppyPlace.Api.Mappings;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, GetPersonDto>();
        CreateMap<Dog, PersonDogsDto>();
    }
}