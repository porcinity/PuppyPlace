using PuppyPlace.Data;
using PuppyPlace.Repository;

namespace PuppyPlace.Service;

public class AdoptionService : IAdoptionService
{
    private readonly IDogsRepository _dogsRepository;
    private readonly PuppyPlaceContext _context;

    public AdoptionService(PuppyPlaceContext puppyPlaceContext, IDogsRepository dogsRepository)
    {
        _context = puppyPlaceContext;
        _dogsRepository = dogsRepository;
    }

    public async Task AdoptDog(Guid personId, Guid dogId)
    {
        var dog = await _dogsRepository.FindDog(dogId);
        
        dog.OwnerId = personId;
        await _dogsRepository.UpdateDog(dog);
        await _context.SaveChangesAsync();
    }
}