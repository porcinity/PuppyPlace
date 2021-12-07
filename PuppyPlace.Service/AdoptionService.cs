using PuppyPlace.Data;

namespace PuppyPlace.Service;

public class AdoptionService : IAdoptionService
{
    private readonly IPersonsService _personsService;
    private readonly IDogsService _dogsService;
    private readonly PuppyPlaceContext _context;

    public AdoptionService(PuppyPlaceContext puppyPlaceContext, IDogsService dogsService, IPersonsService personsService)
    {
        _personsService = personsService;
        _dogsService = dogsService;
        _context = puppyPlaceContext;
    }

    public async Task AdoptDog(Guid personId, Guid dogId)
    {
        var person = await _personsService.FindPerson(personId);
        var dog = await _dogsService.FindDog(dogId);
        
        dog.OwnerId = personId;
        await _dogsService.UpdateDog(dog);
        await _context.SaveChangesAsync();
    }
}