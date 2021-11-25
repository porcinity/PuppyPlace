using Figgle;

namespace PuppyPlace;

public static class Prompt
{
    public static List<Dog> Dogs = new List<Dog>();
    public static List<Person> Persons = new List<Person>();

    public static void MainMenu()
    {
        ShowMainMenuText();
        var userChoice = Console.ReadKey();
        switch (userChoice.Key)
        {
            case ConsoleKey.D1:
                AddPerson();
                break;
            case ConsoleKey.D2:
                AddDog();
                break;
            case ConsoleKey.D3:
                ShowPeople();
                break;
            case ConsoleKey.D4:
                ShowDogs();
                break;
            case ConsoleKey.Q:
                Quit();
                break;
            default:
                PromptAgain();
                break;
        }
    }

    private static void AddPerson()
    {
        Console.Clear();
        Console.WriteLine("Great! Let's add a new person!" +
                          "\n==============================");
        Thread.Sleep(2000);
        Console.Clear();

        var newPerson = CreatePerson();
        
        Persons.Add(newPerson);
        Thread.Sleep(1500);
        
        AddAnotherPerson();
    }

    private static void AddAnotherPerson()
    {
        Console.WriteLine("Add another person? Choose: yes/no");
        var userChoice = Console.ReadLine();

        switch (userChoice)
        {
            case "yes":
                AddPerson();
                break;
            case "no":
                MainMenu();
                break;
            default:
                Console.Clear();
                AddAnotherPerson();
                break;
        }
    }

    private static void AddDog()
    {
        Console.Clear();
        Console.WriteLine("Great! Let's add a new dog!" +
                          "\n=========================");
        
        Thread.Sleep(2000);
        Console.Clear();
        
        var newDog = CreateDog();
        Dogs.Add(newDog);
        Thread.Sleep(1500);
        AddAnotherDog();
    }

    private static void AddAnotherDog()
    {
        Console.WriteLine("Add another dog? Choose: yes/no");
        var userChoice = Console.ReadLine();
        switch (userChoice)
        {
            case "yes":
                AddDog();
                break;
            case "no":
                MainMenu();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                AddAnotherDog();
                break;
        }
    } 
    
    private static Dog CreateDog()
    {
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

        Console.WriteLine("Success! We added the following information to the database:" +
                          "\n==========================================================" +
                          $"\nName: {newDog.Name}" +
                          $"\nAge: {newDogAge}" +
                          $"\nBreed: {newDogBreed}" +
                          $"\n========================================================="
        );

        return newDog;
    }

    private static Person CreatePerson()
    {
        Console.WriteLine("Please insert the person's name:");
        var newPersonName = Console.ReadLine();
        
        Console.Clear();

        var newPerson = new Person(newPersonName);

        Console.WriteLine("Success! We add the following information to the database:" +
                          "\n========================================================" +
                          $"\nName: {newPerson.Name}" +
                          $"\n=======================================================");

        return newPerson;
    }

    private static void ShowDogs()
    {
        Console.Clear();
        Console.WriteLine("These are the dogs we have:");
        
        var num = 1;
        foreach (var dog in Dogs)
        {
            Console.WriteLine($"{num} - {dog.Name}");
            num++;
        }

        Console.WriteLine("Press (m) to return to main menu.");
        var userChoice = Console.ReadKey(true);

        switch (userChoice.Key)
        {
            case ConsoleKey.M:
                MainMenu();
                break;
            default:
                ShowDogs();
                break;
        }
    }

    private static void ShowPeople()
    {
        Console.Clear();
        Console.WriteLine("These are the people in our database:");

        var num = 1;
        foreach (var person in Persons)
        {
            Console.WriteLine($"{num} - {person.Name}");
            num++;
        }
        
        Console.WriteLine("Press (m) to return to main menu.");
        var userPress = Console.ReadKey(true);

        switch (userPress.Key)
        {
            case ConsoleKey.M:
                MainMenu();
                break;
            default:
                ShowPeople();
                break;
        }
    }

    public static void SeedDogs()
    {
        Dogs.Add(new Dog("Apollo", 12, "Dachshund"));
        Dogs.Add(new Dog("Kylie", 15, "Schnauzer"));
        Dogs.Add(new Dog("Remy", 8, "Schnauzer"));
        Dogs.Add(new Dog("Speckles", 12, "Red Heeler"));
        Dogs.Add(new Dog("Bucky", 13, "Forgot"));
    }

    public static void SeedPersons()
    {
        Persons.Add(new Person("Andrea"));
        Persons.Add(new Person("Aaron"));
        Persons.Add(new Person("Cody"));
    }

    static void Quit()
    {
        Console.Clear();
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
    }
    
    static void PromptAgain()
    {
        Console.Clear();
        Console.WriteLine("Invalid choice.");
        Thread.Sleep(1000);
        Console.Clear();
        MainMenu();
    }

    static void ShowMainMenuText()
    {
        Console.WriteLine(FiggleFonts.Doom.Render("Puppy  Place"));
        Console.WriteLine("\nM  A  I  N      M  E  N  U" +
                          "\n==========================" +
                          "\n");
        Console.WriteLine("What would you like to do?" +
                          "\n1 - Add new Person" +
                          "\n2 - Add new Dog" +
                          "\n3 - Show list of People" +
                          "\n4 - Show list of Dogs" +
                          "\n\n(Press q to quit)");
    }
}