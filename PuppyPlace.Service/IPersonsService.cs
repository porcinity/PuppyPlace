using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public interface IPersonsService
{
    Task<List<Person>> FindPersons();
    Task<Person> FindPerson(Guid id);
    // Task AdoptDog(Guid personId, Guid dogId);
    Task AddPerson(Person person);
    Task UpdatePerson(Person person);
    Task DeletePerson(Guid id);
}