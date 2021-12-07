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
        
    private static DogDto ItemToDto(Dog dog) =>
        new DogDto
        {
            Id = dog.Id,
            Name = dog.Name,
            Age = dog.Age,
            Breed = dog.Breed,
            OwnerId = dog.OwnerId
        };

    public async Task<IEnumerable<DogDto>> FindDogs()
    {
        var dog = await _dogsRepository.FindDogs();
        return dog.Select(m => ItemToDto(m)).ToList();
    }

    public async Task<DogDto> FindDog(Guid id)
    {
        var dog = await _dogsRepository.FindDog(id);
        return ItemToDto(dog);
    }
    
    public async Task AddDog(DogDto dogDto)
    {
        var dog = new Dog(dogDto.Name, dogDto.Age, dogDto.Breed);
        await _dogsRepository.AddDog(dog);
    }

    public async Task UpdateDog(DogDto dogDto)
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