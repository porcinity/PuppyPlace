namespace PuppyPlace.Service;

public interface IPersonsService
{
    Task<List<PersonDTO>> FindPersons();
    Task<PersonDTO> FindPerson(Guid id);
    Task AddPerson(PersonDTO personDto);
    // Task DeletePerson();
}