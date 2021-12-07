namespace PuppyPlace.Service;

public interface IPersonsService
{
    Task<IEnumerable<PersonDTO>> FindPersons();
    Task<PersonDTO> FindPerson(Guid id);
    Task AddPerson(PersonDTO personDto);
    // Task DeletePerson();
}