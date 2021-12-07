using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public interface IDogsService
{
    Task<List<DogDTO>> FindDogs();
    Task<Dog> FindDog(Guid id);
    Task<Dog> FindDogWithOwner(Guid id);
    Task AddDog(Dog dog);
    Task UpdateDog(Dog dog);
    Task DeleteDog(Guid id);
}