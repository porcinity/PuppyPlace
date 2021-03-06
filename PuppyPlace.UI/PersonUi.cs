using PuppyPlace.Domain;
using PuppyPlace.Repository;
using PuppyPlace.Service;

namespace PuppyPlace.Ui;

public class PersonUi : IPersonUi
{
    private readonly IPersonsRepository _personsRepository;
    private readonly IDogsRepository _dogsRepository;
    private readonly IAdoptionService _adoptionService;
 

    public PersonUi(IPersonsRepository personsRepository, IDogsRepository dogsRepository, IAdoptionService adoptionService)
    {
        _personsRepository = personsRepository;
        _dogsRepository = dogsRepository;
        _adoptionService = adoptionService;
    }
    public async Task AddPersonPrompt()
    {
        Console.Clear();
        Console.WriteLine("Great! Let's add a new person!" +
                          "\n==============================");
        Thread.Sleep(1000);
        await CreatePerson();
    }
    
    private async Task CreatePerson()
    {
        Console.Clear();
        Console.WriteLine("Please insert the person's name:");
        var newPersonName = Console.ReadLine();
        
        Console.Clear();

        var newPerson = new Person(newPersonName);
        var personId = newPerson.Id;
        try
        {
            await _personsRepository.AddPerson(newPerson);
            var addedPerson = await _personsRepository.FindPerson(personId);
            Console.WriteLine("Success! We add the following information to the database:" +
                              "\n========================================================" +
                              $"\nName: {addedPerson.Name}" +
                              $"\n=======================================================");
            Thread.Sleep(1500);
            await ConsoleMainMenu.Show();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

       
    }
    
    public async Task ShowPersons()
    {
        // ConsoleMainMenu.ShowLoadingAnimation();
        Console.Clear();
        var persons = await _personsRepository.FindPersons();
        var personNum = 1;
        foreach (var person in persons)
        {
            Console.WriteLine($"{personNum} - {person.Name}");
            personNum++;
        }

        Console.WriteLine("Press number or (b)ack");
    
       var userInput = Console.ReadKey();
       
       switch (userInput.Key)
       {
           case ConsoleKey.B:
               await ConsoleMainMenu.Show();
               break;
           default:
               if (int.TryParse(userInput.KeyChar.ToString(), out var charIntEntered))
               {
                   var personIndex = charIntEntered - 1;

                   var person = persons.ElementAtOrDefault(personIndex);

                   if (person == null)
                   {
                       throw new Exception("Person not found at index");
                   }

                   var personId = person.Id;
                   await ShowPerson(personId);
               }

               break;
       }
    }

    public async Task ShowPerson(Guid id)
    {
        while (true)
        {
            var foundPerson = await _personsRepository.FindPerson(id);
            Console.Clear();
            Console.WriteLine("====================" + $"\nName: {foundPerson.Name}");
            if (foundPerson.Dogs.Count() != 0)
            {
                Console.WriteLine("Dogs:");
                foreach (var dog in foundPerson.Dogs)
                {
                    Console.WriteLine($" - {dog.Name}");
                }
            }
            else
            {
                Console.WriteLine("Dogs: none right now");
            }

            Console.WriteLine("===================");

            Console.WriteLine("(a)dopt dog, (u)pdate information, (d)elete person, (b)ack, (m)ain menu, (q)uit");
            var userInput = Console.ReadKey();
            switch (userInput.Key)
            {
                case ConsoleKey.A:
                    await AdoptDog(foundPerson);
                    break;
                case ConsoleKey.D:
                    await DeletePerson(foundPerson);
                    break;
                case ConsoleKey.U:
                    await UpdatePerson(foundPerson);
                    break;
                case ConsoleKey.B:
                    await ShowPersons();
                    break;
                case ConsoleKey.M:
                    await ConsoleMainMenu.Show();
                    break;
                case ConsoleKey.Q:
                    ConsoleMainMenu.Quit();
                    break;
                default:
                    ConsoleMainMenu.ShowInvalidMessage();
                    continue;
            }

            break;
        }
    }

    private async Task AdoptDog(Person person)
    {
        Console.Clear();
        Console.WriteLine("Great! Let's adopt a dog!");
        var doggies = await _dogsRepository.FindDogs();

        var num = 1;
        foreach (var dog in doggies)
        {
            Console.WriteLine($"{num} - {dog.Name}");
            num++;
        }

        Console.WriteLine("Choose dog to adopt:");

        var userChoice = Console.ReadKey();

        if (int.TryParse(userChoice.KeyChar.ToString(), out var choice))
        {
            try
            {
                var dogList = doggies.ToList();
                var dogToAdopt = dogList[choice - 1];
                await _adoptionService.AdoptDog(person.Id, dogToAdopt.Id);
                Console.WriteLine($"Success! {person.Name} now owns {dogToAdopt.Name}!");
            }
            catch (Exception e)
            {
                ConsoleMainMenu.ShowInvalidMessage();
                await AdoptDog(person);
            }
        }

        await ConsoleMainMenu.Show();
    }

    public async Task UpdatePerson(Person person)
    {
        Console.Clear();
        Console.WriteLine($"What information would you like to update for {person.Name}");
        Console.WriteLine("(n)ame");
        var userChoice = Console.ReadKey();

        switch (userChoice.Key)
        {
            case ConsoleKey.N:
                Console.Clear();
                Console.WriteLine($"Enter the updated information for {person.Name}:");
                var updatedField = Console.ReadLine();
                if (updatedField is not null)
                {
                    person.Name = updatedField;
                    await _personsRepository.UpdatePerson(person);
                    Console.WriteLine($"Success! We've updated {person.Name}'s information.");
                }
                break;
            default:
                ConsoleMainMenu.ShowInvalidMessage();
                await UpdatePerson(person);
                break;
        }

        await ConsoleMainMenu.Show();
    }
    
    public async Task DeletePerson(Person person)
    {
       await _personsRepository.DeletePerson(person.Id);
       await ConsoleMainMenu.Show();
    }
}