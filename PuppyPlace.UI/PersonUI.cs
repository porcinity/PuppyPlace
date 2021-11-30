using PuppyPlace.Domain;
using PuppyPlace.Service;

namespace PuppyPlace.UI;

public class PersonUI
{
    private readonly PersonsService _personsService = new PersonsService();
    public void AddPersonPrompt()
    {
        Prompt.ShowLoadingAnimation();
        Console.Clear();
        Console.WriteLine("Great! Let's add a new person!" +
                          "\n==============================");
        Thread.Sleep(1000);
        CreatePerson();
        ConsoleMainMenu.Show();
    }
    
    private async void CreatePerson()
    {
        Console.Clear();
        Console.WriteLine("Please insert the person's name:");
        var newPersonName = Console.ReadLine();
        
        Console.Clear();

        var newPerson = new Person(newPersonName);
        var personId = newPerson.Id;
        _personsService.AddPersonDb(newPerson);

        var addedPerson = await _personsService.FindPerson(personId);
        Console.WriteLine("Success! We add the following information to the database:" +
                          "\n========================================================" +
                          $"\nName: {addedPerson.Name}" +
                          $"\n=======================================================");
        Thread.Sleep(1500);
        
    }
    
    public async void ShowPersons()
    {
        ConsoleMainMenu.ShowLoadingAnimation();
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
               ConsoleMainMenu.Show();
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
                   ShowPerson(personId);
               }

               break;
       }
    }

    public async void ShowPerson(Guid id)
    {
        var foundPerson = await _personsService.FindPerson(id);
        Console.Clear();
        Console.WriteLine("====================" +
                          $"\nName: {foundPerson.Name}");
        if (foundPerson.Dogs.Count == 0)
        {
            Console.WriteLine($"Dogs: Ain't got no mf dogs rn");
        }
        else
        {
            Console.WriteLine("Dogs:");
            foreach (var dog in foundPerson.Dogs)
            {
                Console.WriteLine($" - {dog.Name}");
            }
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
                ShowPersons();
                break;
            case ConsoleKey.M:
                Prompt.ShowMainMenu();
                break;
            case ConsoleKey.Q:
                Prompt.Quit();
                break;
            default:
                Prompt.ShowInvalidMessage();
                ShowPerson(id);
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
                _personsService.UpdatePerson(person.Id, updatedField);
                Console.WriteLine($"Success! We've updated {person.Name}'s information.");
                break;
            default:
                Prompt.ShowInvalidMessage();
                UpdatePerson(person);
                break;
        }
        Thread.Sleep(500);
        ConsoleMainMenu.Show();
    }
    
    public async void DeletePerson(Person person)
    {
       _personsService.DeletePersonDb(person);
       ConsoleMainMenu.Show();
    }
}