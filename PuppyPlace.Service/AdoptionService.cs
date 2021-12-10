using PuppyPlace.Data;
using PuppyPlace.Repository;

namespace PuppyPlace.Service;

public class AdoptionService : IAdoptionService
{
    private readonly IPersonsService _personsService;
    private readonly IDogsRepository _dogsRepository;
    private readonly IDogsService _dogsService;
    private readonly PuppyPlaceContext _context;

    public AdoptionService(PuppyPlaceContext puppyPlaceContext, IDogsService dogsService, IPersonsService personsService, IDogsRepository dogsRepository)
    {
        _personsService = personsService;
        _dogsRepository = dogsRepository;
        _dogsService = dogsService;
        _context = puppyPlaceContext;
    }

    public async Task AdoptDog(Guid personId, Guid dogId)
    {
        // var person = await _personsService.FindPerson(personId);
        var dog = await _dogsRepository.FindDog(dogId);
        
        dog.OwnerId = personId;
        await _dogsRepository.UpdateDog(dog);
        await _context.SaveChangesAsync();
    }
}