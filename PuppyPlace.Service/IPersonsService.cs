namespace PuppyPlace.Service;

public interface IPersonsService
{
    Task<IEnumerable<PersonDto>> FindPersons();
    Task<PersonDto> FindPerson(Guid id);
    Task AddPerson(PersonDto personDto);
    Task UpdatePerson(PersonDto personDto);
    Task DeletePerson(Guid id);
}