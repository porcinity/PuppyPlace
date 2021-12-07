using Microsoft.EntityFrameworkCore;
using PuppyPlace.Domain;
using PuppyPlace.Repository;

namespace PuppyPlace.Service;

public class DogsService : IDogsService
{
    private readonly IDogsRepository _dogsRepository;

    public DogsService(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }
        
    private static DogDTO ItemToDTO(Dog dog) =>
        new DogDTO
        {
            Id = dog.Id,
            Name = dog.Name,
            Age = dog.Age,
            Breed = dog.Breed,
            OwnerId = dog.OwnerId
        };

    public async Task<IEnumerable<DogDTO>> FindDogs()
    {
        var dog = await _dogsRepository.FindDogs();
        return dog.Select(m => ItemToDTO(m)).ToList();
    }

    public async Task<DogDTO> FindDog(Guid id)
    {
        var dog = await _dogsRepository.FindDog(id);
        return ItemToDTO(dog);
    }
    
    public async Task AddDog(DogDTO dogDto)
    {
        var dog = new Dog(dogDto.Name, dogDto.Age, dogDto.Breed);
        await _dogsRepository.AddDog(dog);
    }

    public async Task UpdateDog(DogDTO dogDto)
    {
        var dog = await _dogsRepository.FindDog(dogDto.Id);
        dog.Id = dogDto.Id;
        dog.Name = dogDto.Name;
        dog.Age = dogDto.Age;
        dog.Breed = dogDto.Breed;
        dog.OwnerId = dogDto.OwnerId;
        await _dogsRepository.UpdateDog(dog);
    }

    public async Task DeleteDog(Guid id)
    {
        await _dogsRepository.DeleteDog(id);
    }
}