using PuppyPlace.Data;

namespace PuppyPlace.Service;

public class AdoptionService : IAdoptionService
{
    private readonly PersonsService _personsService;
    private readonly DogsService _dogsService;
    // private readonly PuppyPlaceContext _context;

    public AdoptionService(DogsService dogsService, PersonsService personsService)
    {
        _personsService = personsService;
        _dogsService = dogsService;
    }

    public async Task AdoptDog(Guid personId, Guid dogId)
    {
        var person = await _personsService.FindPerson(personId);
        var dog = await _dogsService.FindDog(dogId);
        
        await _dogsService.AddOwner(dog, person);
        // await _context.SaveChangesAsync();
    }
}