using Figgle;

namespace PuppyPlace;

public static class Prompt
{
    public static void ShowMainMenu()
    {
        Console.Clear();
        ShowMainMenuText();
        var userChoice = Console.ReadKey();
        switch (userChoice.Key)
        {
            case ConsoleKey.D1:
                PersonTools.AddPerson();
                break;
            case ConsoleKey.D2:
                DogTools.AddDog();
                break;
            case ConsoleKey.D3:
                PersonTools.ShowPeople();
                break;
            case ConsoleKey.D4:
                DogTools.ShowDogs();
                break;
            case ConsoleKey.Q:
                Quit();
                break;
            default:
                ShowInvalidMessage();
                ShowMainMenu();
                break;
        }
    }
    public static List<Dog> Dogs = new List<Dog>()
    {
        new Dog("Apollo", 12, "Dachshund"),
        new Dog("Kylie", 15, "Schnauzer"),
        new Dog("Remy", 8, "Schnauzer"),
        new Dog("Speckles", 12, "Red Heeler"),
        new Dog("Bucky", 15, "Fox Terrier")
    };
    public static List<Person> Persons = new List<Person>
    {
        new Person("Andrea"),
        new Person("Aaron"),
        new Person("Cody")
    };
    public static void Quit()
    {
        Console.Clear();
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
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
    
    public static void ShowInvalidMessage()
    {
        Console.Clear();
        Console.WriteLine("Invalided choice.");
        Thread.Sleep(1000);
    }
}