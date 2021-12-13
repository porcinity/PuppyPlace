using PuppyPlace.Data;
using PuppyPlace.Repository;
using PuppyPlace.Service;

namespace PuppyPlace.Ui;

public static class DI
{
    public static readonly PuppyPlaceContext PuppyPlaceContext = new ();
    
    public static readonly IPersonsRepository PersonsRepository = new PersonsRepository(PuppyPlaceContext);
    public static readonly IDogsRepository DogsRepository = new DogsRepository(PuppyPlaceContext);
    
    public static readonly IPersonsService PersonsService = new PersonsService(PersonsRepository);
    public static readonly IDogsService DogsService = new DogsService(DogsRepository);
    
    public static readonly PersonUi PersonUi = new PersonUi(
        new PersonsRepository(PuppyPlaceContext),
        new DogsRepository(PuppyPlaceContext),
        new AdoptionService(PuppyPlaceContext, DogsRepository));
    
    public static readonly DogsUi DoggieUi = new DogsUi(DogsRepository, PersonsRepository);
}