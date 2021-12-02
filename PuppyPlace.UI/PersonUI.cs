using PuppyPlace.Domain;
using PuppyPlace.Service;

namespace PuppyPlace.UI;

public class PersonUI
{
    private readonly PersonsService _personsService;
    
    public PersonUI(PersonsService personsService)
    {
        _personsService = personsService;
    }
    public async Task AddPersonPrompt()
    {
        ConsoleMainMenu.ShowLoadingAnimation();
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
            await _personsService.AddPersonDb(newPerson);
            var addedPerson = await _personsService.FindPerson(personId);
            Console.WriteLine("Success! We add the following information to the database:" +
                              "\n========================================================" +
                              $"\nName: {addedPerson.Name}" +
                              $"\n=======================================================");
            Thread.Sleep(1500);
            ConsoleMainMenu.Show();
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
        var persons = await _personsService.FindPersons();
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
            var foundPerson = await _personsService.FindPerson(id);
            Console.Clear();
            Console.WriteLine("====================" + $"\nName: {foundPerson.Name}");
            if (foundPerson.Dogs.Count != 0)
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

            Console.WriteLine("(u)pdate information, (d)elete person, (b)ack, (m)ain menu, (q)uit");
            var userInput = Console.ReadKey();
            switch (userInput.Key)
            {
                case ConsoleKey.D:
                    DeletePerson(foundPerson);
                    break;
                case ConsoleKey.U:
                    UpdatePerson(foundPerson);
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

    public void UpdatePerson(Person person)
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
                person.Name = updatedField;
                _personsService.UpdatePerson(person);
                Console.WriteLine($"Success! We've updated {person.Name}'s information.");
                break;
            default:
                ConsoleMainMenu.ShowInvalidMessage();
                UpdatePerson(person);
                break;
        }
        Thread.Sleep(500);
        ConsoleMainMenu.Show();
    }
    
    public async Task DeletePerson(Person person)
    {
       await _personsService.DeletePersonDb(person);
       ConsoleMainMenu.Show();
    }
}