using PuppyPlace.Domain;

namespace PuppyPlace.Repository;

public interface IPersonsRepository
{
    Task<IEnumerable<Person>> FindPersons();
    Task<Person> FindPerson(Guid id);
    Task AddPerson(Person person);
    Task UpdatePerson(Person person);
    Task DeletePerson(Guid id);
}