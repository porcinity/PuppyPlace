using LanguageExt;

namespace PuppyPlace.Repository;

public interface IAdoptionService
{
    Task<Option<Unit>> AdoptDog(Guid personId, Guid dogId);
}