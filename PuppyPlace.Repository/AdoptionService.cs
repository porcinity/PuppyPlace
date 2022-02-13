using PuppyPlace.Data;
using LanguageExt;
using static LanguageExt.Prelude;

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

    public async Task<Option<Unit>> AdoptDog(Guid personId, Guid dogId)
    {
        var dog = await _dogsRepository.FindDog(dogId);
        var person = await _personsRepository.FindPerson(personId);

        var result = dog.Map(async d =>
        {
            person.AddDog(d);
            await _context.SaveChangesAsync();
        });

        return dog.Map(x => unit);
    }
}