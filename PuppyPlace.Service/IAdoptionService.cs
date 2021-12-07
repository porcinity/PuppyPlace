namespace PuppyPlace.Service;

public interface IAdoptionService
{
    Task AdoptDog(Guid personId, Guid dogId);
}