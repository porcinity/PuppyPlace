using PuppyPlace.Domain;

namespace PuppyPlace.Repository;

public interface IDogsRepository
{
    IQueryable<Dog> FindDogs();
    Task<Dog> FindDog(Guid id);
    Task<Dog> FindDogWithOwner(Guid id);
    Task AddDog(Dog dog);
    Task UpdateDog(Dog dog);
    Task DeleteDog(Guid id);
}