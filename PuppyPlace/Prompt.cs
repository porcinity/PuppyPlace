using Figgle;

namespace PuppyPlace;

public static class Prompt
{
    public static List<Dog> Dogs = new List<Dog>();
    public static List<Person> Persons = new List<Person>();

    public static void ShowMainMenu()
    {
        ShowMainMenuText();
        var userChoice = Console.ReadKey();
        switch (userChoice.Key)
        {
            case ConsoleKey.D1:
                Person.AddPerson();
                break;
            case ConsoleKey.D2:
                Dog.AddDog();
                break;
            case ConsoleKey.D3:
                Person.ShowPeople();
                break;
            case ConsoleKey.D4:
                Dog.ShowDogs();
                break;
            case ConsoleKey.Q:
                Quit();
                break;
            default:
                PromptAgain();
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
        ShowMainMenu();
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