using Microsoft.EntityFrameworkCore;
using PuppyPlace.Repository;

namespace PuppyPlace.Domain;

public class DogsService
{
    private readonly DogsRepository _dogsRepository;

    public DogsService(DogsRepository dogsRepository)
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
        return await _dogsRepository.FindDogs()
            .Select(m => ItemToDTO(m))
            .ToListAsync();
    }

    public async Task<DogDTO> FindDog(Guid id)
    {
        var dog = await _dogsRepository.FindDog(id);
        return ItemToDTO(dog);
    }
}