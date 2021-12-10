using PuppyPlace.Domain;

namespace PuppyPlace.Ui;

public interface IDogsUi
{
    Task AddDog();
    Task ShowDogs();
    Task SelectDog(IEnumerable<Dog> dogs, char choice);
    Task ShowDog(Dog dog);
    Task AddOwnerToDog(Dog dog);
    Task DeleteDog(Dog dog);
}