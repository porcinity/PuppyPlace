namespace PuppyPlace.Repository;

public interface IAdoptionService
{
    Task AdoptDog(Guid personId, Guid dogId);
}