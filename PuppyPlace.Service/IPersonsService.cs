using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public interface IPersonsService
{
    Task<List<Person>> FindPersons();
    Task<Person> FindPerson(Guid id);
    Task AddPerson(Person person);
    Task UpdatePerson(Person person);
    Task DeletePerson(Person person);
}