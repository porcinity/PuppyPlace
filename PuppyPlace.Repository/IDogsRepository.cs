using LanguageExt;
using PuppyPlace.Domain;

namespace PuppyPlace.Repository;

public interface IDogsRepository
{
    Task<IList<Dog>> FindDogs();
    Task<Option<Dog>> FindDog(Guid id);
    Task<Option<Dog>> FindDogWithOwner(Guid id);
    Task<Unit> AddDog(Dog dog);
    Task<Unit> UpdateDog(Dog dog);
    Task<Option<Unit>> DeleteDog(Guid id);
    Task<Unit> DeleteDog(Dog dog);
}