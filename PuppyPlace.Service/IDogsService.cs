using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public interface IDogsService
{
    Task<List<Dog>> FindDogs();
    Task<Dog> FindDog(Guid id);
    Task AddDog(Dog dog);
    Task DeleteDog(Guid id);
}