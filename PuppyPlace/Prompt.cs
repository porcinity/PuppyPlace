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

        Console.WriteLine("Enter name of dog for more information, or (b) to go back:");
        var userInput = Console.ReadLine();

        switch (userInput)
        {
            case "b":
                MainMenu();
                break;
            default:
                try
                {
                    ShowDog(userInput);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Dog not found!");
                    Thread.Sleep(1000);
                    ShowDogs();
                }
                break;
        }
    }

    static void ShowDog(string dogName)
    {
        var foundDog = Dogs.Find(x => x.Name.ToLower() == dogName.ToLower());
        Console.Clear();
        Console.WriteLine("======================\n" +
                          $"Name: {foundDog.Name}\n" +
                          $"Age: {foundDog.Age}\n" +
                          $"Breed: {foundDog.Breed}");

        try
        {
            Console.WriteLine($"Owner: {foundDog.Owner.Name}");
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine($"Owner: {foundDog.Name} does not have an owner yet!");
        }

        Console.WriteLine("======================");

        Console.WriteLine("Enter (a) to add owner (b) to go back:");
        var userChoice = Console.ReadLine();
        switch (userChoice)
        {
            case "a":
                AddOwnerToDog(foundDog);
                break;
            case "b":
                ShowDogs();
                break;
            case "u":
                UpdateDog(foundDog);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                Thread.Sleep(1000);
                ShowDog(dogName);
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

    static void AddOwnerToDog(Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"Great! Who is adopting {dog.Name}?");
        var num = 1;
        foreach (var person in Persons)
        {
            Console.WriteLine($"{num} - {person.Name}");
            num++;
        }
        Console.WriteLine("Enter name of person:");
        var userChoice = Console.ReadLine();
        var adoptingPerson = Persons.Find(x => x.Name == userChoice);
        dog.AddOwner(adoptingPerson);
        var newOwner = dog.Owner.Name;
        Console.WriteLine($"Success! {dog.Name} now belongs to {newOwner}");
        Thread.Sleep(1000);
        MainMenu();
    }

    static void UpdateDog(Dog dog)
    {
        Console.WriteLine("What would you like to update?");
        Console.WriteLine("(N)ame, (A)ge, (B)reed");
        var userChoice = Console.ReadKey();
    
        switch (userChoice.Key)
        {
            case ConsoleKey.N:
                Console.Clear();
                Console.WriteLine("Enter updated name:");
                var updatedName = Console.ReadLine();
                dog.Name = updatedName;
                Console.WriteLine($"Success! Name has been update to: {dog.Name}");
                Console.ReadLine();
                MainMenu();
                break;
            case ConsoleKey.A:
                Console.Clear();
                Console.WriteLine("Enter updated age:");
                var updatedAge = Console.ReadLine();
                var ageToInt = Int32.Parse(updatedAge);
                dog.Age = ageToInt;
                Console.WriteLine($"Success! Name has been update to: {dog.Name}");
                Console.ReadLine();
                MainMenu();
                break;
            default:
                Console.WriteLine("Invalided choice.");
                Thread.Sleep(1000);
                UpdateDog(dog);
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