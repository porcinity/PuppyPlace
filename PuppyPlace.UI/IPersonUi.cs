using PuppyPlace.Domain;

namespace PuppyPlace.Ui;

public interface IPersonUi
{
    Task AddPersonPrompt();
    Task ShowPersons();
    Task ShowPerson(Guid id);
    Task UpdatePerson(Person person);
    Task DeletePerson(Person person);
}