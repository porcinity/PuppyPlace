using PuppyPlace.Domain;

namespace PuppyPlace.UI;

public static class PersonTools
{
    public static List<Person> Persons = new List<Person>
    {
        new ("Andrea"),
        new ("Aaron"),
        new ("Cody")
    };
    public static void AddPerson()
    {
        Prompt.ShowLoadingAnimation();
        Console.Clear();
        Console.WriteLine("Great! Let's add a new person!" +
                          "\n==============================");
        Thread.Sleep(1000);
        CreatePerson();
        ShowPromptToAddAnotherPerson();
    }

    private static void ShowPromptToAddAnotherPerson()
    {
        Console.WriteLine("Add another person? Choose: yes/no");
        var userChoice = Console.ReadLine();

        switch (userChoice)
        {
            case "yes":
                AddPerson();
                break;
            case "no":
                Prompt.ShowMainMenu();
                break;
            default:
                Console.Clear();
                ShowPromptToAddAnotherPerson();
                break;
        }
    }
    private static void CreatePerson()
    {
        Console.Clear();
        Console.WriteLine("Please insert the person's name:");
        var newPersonName = Console.ReadLine();
        
        Console.Clear();

        var newPerson = new Person(newPersonName);
        Persons.Add(newPerson);

        Console.WriteLine("Success! We add the following information to the database:" +
                          "\n========================================================" +
                          $"\nName: {newPerson.Name}" +
                          $"\n=======================================================");
        Thread.Sleep(1500);
    }
    public static void ShowPeople()
    {
        Prompt.ShowLoadingAnimation();
        Console.Clear();
        Console.WriteLine("These are the people in our database:");

        var num = 1;
        foreach (var person in Persons)
        {
            Console.WriteLine($"{num} - {person.Name}");
            num++;
        }
        
        Console.WriteLine("(s)elect person, (b)ack");
        var userInput = Console.ReadKey();

        switch (userInput.Key)
        {
            case ConsoleKey.B:
                Prompt.ShowMainMenu();
                break;
            case ConsoleKey.S:
                Console.WriteLine("\nEnter the name of the person you want to select:");
                var person = Console.ReadLine();
                try
                {
                    ShowPerson(person);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Person not found!");
                    Thread.Sleep(1500);
                    ShowPeople();
                }
                break;
            default:
                Prompt.ShowInvalidMessage();
                ShowPeople();
                break;
        }
    }

    static void ShowPerson(string name)
    {
        var foundPerson = Persons.Find(x => x.Name.ToLower() == name.ToLower());
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

        Console.WriteLine("(u)pdate information, (b)ack, (m)ain menu, (q)uit");
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
                ShowPeople();
                break;
            case ConsoleKey.M:
                Prompt.ShowMainMenu();
                break;
            case ConsoleKey.Q:
                Prompt.Quit();
                break;
            default:
                Prompt.ShowInvalidMessage();
                ShowPerson(name);
                break;
        }
    }
    static void UpdatePerson(Person person)
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
                Console.WriteLine($"Success! We've updated {person.Name}'s information.");
                break;
            default:
                Prompt.ShowInvalidMessage();
                UpdatePerson(person);
                break;
        }
        Thread.Sleep(1500);
        Prompt.ShowMainMenu();
    }
    
    static void DeletePerson(Person person)
    {
        Console.Clear();
        Console.WriteLine($"Are you sure you want to delete {person.Name}?" +
                          $"\n(y)es/(n)o");
        var userInput = Console.ReadKey();
        switch (userInput.Key)
        {
            case ConsoleKey.Y:
                Console.Clear();
                Persons.Remove(person);
                foreach (var dog in person.Dogs)
                {
                    dog.Owner = null;
                }
                Console.WriteLine($"Success! We have deleted {person.Name} from our database.");
                break;
            case ConsoleKey.N:
                ShowPerson(person.Name);
                break;
            default:
                Prompt.ShowInvalidMessage();
                DeletePerson(person);
                break;
        }
        Thread.Sleep(1500);
        ShowPeople();
    }
}