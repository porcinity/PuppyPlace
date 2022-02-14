using LanguageExt;
using PuppyPlace.Domain;

namespace PuppyPlace.Repository;

public interface IPersonsRepository
{
    Task<IEnumerable<Person>> FindPersons();
    Task<Option<Person>> FindPerson(Guid id);
    Task<Unit> AddPerson(Person person);
    Task<Unit> UpdatePerson(Person person);
    Task<Option<Unit>> DeletePerson(Guid id);
    Task<Unit> DeletePerson(Person person);
}