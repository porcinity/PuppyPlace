using PuppyPlace.Domain;
using PuppyPlace.Service;

namespace PuppyPlace.UI;

public class DogsUI
{
    private readonly DogsService _dogsService;

    private readonly PersonsService _personsService;

    public DogsUI(DogsService dogsService, PersonsService personsService)
    {
        _dogsService = dogsService;
        _personsService = personsService;
    }
    public async Task AddDog()
    {
        ConsoleMainMenu.ShowLoadingAnimation();
        Console.Clear();
        Console.WriteLine("Great! Let's add a new dog!" +
                          "\n=========================");
        
        Thread.Sleep(1000);
        await CreateDog();
    }
    
    private async Task CreateDog()
    {
        Console.Clear();
        Console.WriteLine("Please insert the dog's name:");
        var newDogName = Console.ReadLine();
        
        Console.Clear();
        
        Console.WriteLine($"Please insert {newDogName}'s age:");
        var newDogAge = Console.ReadLine();
        var intAge = Int32.Parse(newDogAge);
        
        Console.Clear();
        
        Console.WriteLine($"Please insert {newDogName}'s breed:");
        var newDogBreed = Console.ReadLine();

        
        Console.Clear();
        
        var newDog = new Dog(newDogName, intAge, newDogBreed);
        await _dogsService.AddDogDb(newDog);

        Console.WriteLine("Success! We added the following information to the database:" +
                          "\n==========================================================" +
                          $"\nName: {newDog.Name}" +
                          $"\nAge: {newDogAge}" +
                          $"\nBreed: {newDogBreed}" +
                          $"\n========================================================="
        );
        await ConsoleMainMenu.Show();
    }

    public async Task ShowDogs()
    {
        Console.Clear();
        var dogs = await _dogsService.FindDogs();
        var num = 1;
        if (dogs.Count != 0)
        {
            foreach (var dog in dogs)
            {
                Console.WriteLine($"{num} - {dog.Name}");
                num++;
            }
        }
        else
        {
            Console.WriteLine("We don't have any dogs right now!");
        }

        Console.WriteLine("Press number for more information or (b)ack");
        var userInput = Console.ReadKey();
        switch (userInput.Key)
        {
            case ConsoleKey.B:
                await ConsoleMainMenu.Show();
                break;
            default:
                await SelectDog(dogs, userInput.KeyChar);
                break;
        }
    }

    public async Task SelectDog(List<Dog> dogs, char choice)
    {
        if (int.TryParse(choice.ToString(), out var charIntEntered))
        {
            var dogIndex = charIntEntered - 1;
            var dog = dogs.ElementAtOrDefault(dogIndex);

            if (dog == null)
            {
                ConsoleMainMenu.ShowInvalidMessage();
                ConsoleMainMenu.Show();
            }
            await ShowDog(dog);
        }
    }

    public async Task ShowDog(Dog dog)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("======================\n" + $"Name: {dog.Name}\n" + $"Age: {dog.Age}\n" + $"Breed: {dog.Breed}");

            try
            {
                Console.WriteLine($"Owner: {dog.Owner.Name}");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Owner: {dog.Name} does not have an owner yet!");
            }

            Console.WriteLine("======================");

            Console.WriteLine("(a)dd owner, (u)pdate information, (d)elete, (b)ack:");
            var userChoice = Console.ReadKey();
            switch (userChoice.Key)
            {
                case ConsoleKey.A:
                    await AddOwnerToDog(dog);
                    break;
                case ConsoleKey.B:
                    await ShowDogs();
                    break;
                // case ConsoleKey.U:
                //     UpdateDog(foundDog);
                //     break;
                case ConsoleKey.D:
                    DeleteDog(dog);
                    break;
                default:
                    ConsoleMainMenu.ShowInvalidMessage();
                    continue;
            }

            break;
        }
    }

    public async Task AddOwnerToDog(Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"Great! Who is adopting {dog.Name}?");
        var persons = await _personsService.FindPersons();
        var num = 1;
        foreach (var person in persons)
        {
            Console.WriteLine($"{num} - {person.Name}");
            num++;
        }
        Console.WriteLine("Enter name of person:");
        var userChoice = Console.ReadKey();

        if (int.TryParse(userChoice.KeyChar.ToString(), out var choice))
        {
            try
            {
                var adoptingPerson = persons[choice - 1];
                // dog.Owner = adoptingPerson;
                await _dogsService.AddOwnerDb(dog, adoptingPerson);
                var newOwner = dog.Owner.Name;
                Console.WriteLine($"Success! {dog.Name} now belongs to {newOwner}");
            }
            catch
            {
                ConsoleMainMenu.ShowInvalidMessage();
                await AddOwnerToDog(dog);
            }
        }
        
        Thread.Sleep(1000);
        await ConsoleMainMenu.Show();
    }

    public async void DeleteDog(Dog dog)
    {
        _dogsService.DeleteDogDb(dog);
        ShowDogs();
    }
}