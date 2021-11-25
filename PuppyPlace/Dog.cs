namespace PuppyPlace;

public class Dog
{
    private Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }
    public Person? Owner { get; set; }

    public Dog(string name, int age, string breed)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
        Breed = breed;
    }
        
    public string Bark()
    {
        return "WOOF!!!";
    }

    public void AddOwner(Person owner)
    {
        Owner = owner;
    }

    public static void ShowDogs()
    {
        Console.Clear();
        Console.WriteLine("These are the dogs we have:");
        
        var num = 1;
        foreach (var dog in Prompt.Dogs)
        {
            Console.WriteLine($"{num} - {dog.Name}");
            num++;
        }

        Console.WriteLine("Enter name of dog for more information, or (b) to go back:");
        var userInput = Console.ReadLine();

        switch (userInput)
        {
            case "b":
                Prompt.MainMenu();
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
        var foundDog = Prompt.Dogs.Find(x => x.Name.ToLower() == dogName.ToLower());
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
    
    static void AddOwnerToDog(Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"Great! Who is adopting {dog.Name}?");
        var num = 1;
        foreach (var person in Prompt.Persons)
        {
            Console.WriteLine($"{num} - {person.Name}");
            num++;
        }
        Console.WriteLine("Enter name of person:");
        var userChoice = Console.ReadLine();
        var adoptingPerson = Prompt.Persons.Find(x => x.Name == userChoice);
        dog.AddOwner(adoptingPerson);
        var newOwner = dog.Owner.Name;
        Console.WriteLine($"Success! {dog.Name} now belongs to {newOwner}");
        Thread.Sleep(1000);
        Prompt.MainMenu();
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
                Prompt.MainMenu();
                break;
            case ConsoleKey.A:
                Console.Clear();
                Console.WriteLine("Enter updated age:");
                var updatedAge = Console.ReadLine();
                var ageToInt = Int32.Parse(updatedAge);
                dog.Age = ageToInt;
                Console.WriteLine($"Success! Name has been update to: {dog.Name}");
                Console.ReadLine();
                Prompt.MainMenu();
                break;
            default:
                Console.WriteLine("Invalided choice.");
                Thread.Sleep(1000);
                UpdateDog(dog);
                break;
        }
    }
}
