using PuppyPlace.Data;

namespace PuppyPlace.Repository;

public class AdoptionService : IAdoptionService
{
    private readonly IDogsRepository _dogsRepository;
    private readonly IPersonsRepository _personsRepository;
    private readonly PuppyPlaceContext _context;

    public AdoptionService(PuppyPlaceContext puppyPlaceContext, IDogsRepository dogsRepository, IPersonsRepository personsRepository)
    {
        _context = puppyPlaceContext;
        _dogsRepository = dogsRepository;
        _personsRepository = personsRepository;
    }

    public async Task AdoptDog(Guid personId, Guid dogId)
    {
        var dog = await _dogsRepository.FindDog(dogId);
        var person = await _personsRepository.FindPerson(personId);
        
        person.AddDog(dog);
        // dog.OwnerId = personId;
        await _dogsRepository.UpdateDog(dog);
        await _context.SaveChangesAsync();
    }
}