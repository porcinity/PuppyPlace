using AutoMapper;
using PuppyPlace.Api.Dtos;
using PuppyPlace.Domain;

namespace PuppyPlace.Api.Mappings;

public class DogProfile : Profile
{
    public DogProfile()
    {
        CreateMap<Person, DogOwnerDto>();
        CreateMap<Dog, GetDogDto>();
        CreateMap<Dog, PostDogDto>();
    }
}