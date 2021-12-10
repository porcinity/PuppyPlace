using PuppyPlace.Domain;
using PuppyPlace.Repository;
using PuppyPlace.Service;

namespace PuppyPlace.Ui;

public class DogsUi : IDogsUi
{
    private readonly IDogsRepository _dogsRepository;
    private readonly IPersonsRepository _personsRepository;
    private readonly IMainMenu _mainMenu;

    public DogsUi(IDogsRepository dogsRepository, IPersonsRepository personsRepository, IMainMenu mainMenu)
    {
        _dogsRepository = dogsRepository;
        _personsRepository = personsRepository;
        _mainMenu = mainMenu;
    }
    public async Task AddDog()
    {
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

        Console.Clear();
        
        Console.WriteLine($"Please insert {newDogName}'s breed:");
        var newDogBreed = Console.ReadLine();

        
        Console.Clear();
        if (newDogAge is not null && newDogName is not null && newDogBreed is not null)
        {
            var intAge = int.Parse(newDogAge);
            var newDog = new Dog(newDogName, intAge, newDogBreed);
            await _dogsRepository.AddDog(newDog);

            Console.WriteLine("Success! We added the following information to the database:" +
                              "\n==========================================================" +
                              $"\nName: {newDog.Name}" +
                              $"\nAge: {newDogAge}" +
                              $"\nBreed: {newDogBreed}" +
                              $"\n========================================================="
            );
            Thread.Sleep(1500);
            await _mainMenu.Return();
        }
       
    }

    public async Task ShowDogs()
    {
        Console.Clear();
        var dogs = await _dogsRepository.FindDogs();
        // var dogs = await _dogsService.ListDogNames();
        var num = 1;
        if (dogs.Count() != 0)
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
                await _mainMenu.Return();
                break;
            default:
                await SelectDog(dogs, userInput.KeyChar);
                break;
        }
    }

    public async Task SelectDog(IEnumerable<Dog> dogs, char choice)
    {
        if (int.TryParse(choice.ToString(), out var charIntEntered))
        {
            var dogIndex = charIntEntered - 1;
            var dog = dogs.ElementAtOrDefault(dogIndex);
            if (dog is not null)
            {
                try
                {
                    await ShowDog(await _dogsRepository.FindDogWithOwner(dog.Id));
                }
                catch (Exception e)
                {
                    _mainMenu.ShowInvalidMessage();
                    await _mainMenu.Return();
                }
            }
            
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
                case ConsoleKey.U:
                    await UpdateDogPrompt(dog);
                    break;
                case ConsoleKey.D:
                    await DeleteDog(dog);
                    break;
                default:
                    _mainMenu.ShowInvalidMessage();
                    continue;
            }

            break;
        }
    }

    public async Task AddOwnerToDog(Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"Great! Who is adopting {dog.Name}?");
        var persons = await _personsRepository.FindPersons();
        var num = 1;
        foreach (var person in persons)
        {
            Console.WriteLine($"{num} - {person.Name}");
            num++;
        }
        Console.WriteLine("Select person:");
        var userChoice = Console.ReadKey();

        if (int.TryParse(userChoice.KeyChar.ToString(), out var choice))
        {
            try
            {
                var personList = persons.ToList();
                var adoptingPerson = personList[choice - 1];
                // dog.Owner = adoptingPerson;
                
                // Fix this
                // await _dogsRepository.AddOwner(dog, adoptingPerson);
                var newOwner = dog.Owner.Name;
                Console.WriteLine($"Success! {dog.Name} now belongs to {newOwner}");
            }
            catch
            {
                _mainMenu.ShowInvalidMessage();
                await AddOwnerToDog(dog);
            }
        }

        await _mainMenu.Return();
    }

    private async Task UpdateDogPrompt(Dog dog)
    {
        Console.Clear();
        Console.WriteLine("What would you like to update?");
        Console.WriteLine("(n)ame, (a)ge, (b)reed, (c)ancel update");
        var userInput = Console.ReadKey();
        while (true)
        {
            switch (userInput.Key)
            {
                case ConsoleKey.N:
                    await UpdateName(dog);
                    break;
                case ConsoleKey.A:
                    await UpdateAge(dog);
                    break;
                case ConsoleKey.B:
                    await UpdateBreed(dog);
                    break;
                case ConsoleKey.C:
                    await _mainMenu.Return();
                    break;
                default:
                    _mainMenu.Return();
                    continue;
            }
            break;
        }
    }
    
    private async Task UpdateName(Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"Previous: {dog.Name}");
        Console.WriteLine("Enter updated name:");
        var userInput = Console.ReadLine();
        if (userInput is not null)
        {
            dog.Name = userInput;
            await _dogsRepository.UpdateDog(dog);
            Console.WriteLine("Success!");
        }

        await _mainMenu.Return();
    }

    private async Task UpdateAge(Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"Previous: {dog.Age}");
        Console.WriteLine("Enter updated age:");
        var userInput = Console.ReadLine();
        if (userInput is not null)
        {
            var intAge = int.Parse(userInput);
            dog.Age = intAge;
            await _dogsRepository.UpdateDog(dog);
            Console.WriteLine("Success!");
        }

        await _mainMenu.Return();
    }
    
    private async Task UpdateBreed(Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"Previous: {dog.Breed}");
        Console.WriteLine("Enter updated breed:");
        var userInput = Console.ReadLine();
        if (userInput is not null)
        {
            dog.Breed = userInput;
            await _dogsRepository.UpdateDog(dog);
            Console.WriteLine("Success!");
        }

        await _mainMenu.Return();
    }

    public async Task DeleteDog(Dog dog)
    {
        await _dogsRepository.DeleteDog(dog.Id);
        await ShowDogs();
    }
}