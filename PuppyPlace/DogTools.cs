namespace PuppyPlace;

public static class DogTools
{
    public static void ShowDogs()
    {
        Console.Clear();
        Console.WriteLine("These are the dogs we have:");
        
        var num = 1;
        if (Prompt.Dogs.Count == 0)
        {
            Console.WriteLine("We don't have any dogs at the moment!");
        }
        else
        {
            foreach (var dog in Prompt.Dogs)
            {
                Console.WriteLine($"{num} - {dog.Name}");
                num++;
            }
        }

        Console.WriteLine("(s)elect dog, (b)ack");
        var userInput = Console.ReadKey();

        switch (userInput.Key)
        {
            case ConsoleKey.B:
                Prompt.ShowMainMenu();
                break;
            case ConsoleKey.S:
                Console.WriteLine("\nEnter name of dog for more information:");
                var doggie = Console.ReadLine();
                try
                {
                    ShowDog(doggie);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Dog not found!");
                    Thread.Sleep(1000);
                    ShowDogs();
                }
                break;
            default:
                Console.Clear();
                Console.WriteLine("Invalid choice.");
                Thread.Sleep(1500);
                ShowDogs();
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

        Console.WriteLine("(a)dd owner, (u)pdate information, (d)elete, (b)ack:");
        var userChoice = Console.ReadKey();
        switch (userChoice.Key)
        {
            case ConsoleKey.A:
                AddOwnerToDog(foundDog);
                break;
            case ConsoleKey.B:
                ShowDogs();
                break;
            case ConsoleKey.U:
                UpdateDog(foundDog);
                break;
            case ConsoleKey.D:
                DeleteDog(foundDog);
                break;
            default:
                Prompt.ShowInvalidMessage();
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
        var adoptingPerson = Prompt.Persons.Find(x => x.Name.ToLower() == userChoice.ToLower());
        dog.AddOwner(adoptingPerson);
        adoptingPerson.AddDog(dog);
        var newOwner = dog.Owner.Name;
        Console.WriteLine($"Success! {dog.Name} now belongs to {newOwner}");
        Thread.Sleep(1000);
        Prompt.ShowMainMenu();
    }

    static void UpdateDog(Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"What information would you like to update for {dog.Name}");
        Console.WriteLine($"(n)ame, (a)ge, (b)reed");
        var userChoice = Console.ReadKey();
    
        switch (userChoice.Key)
        {
            case ConsoleKey.N:
                UpdatePrompt("name", dog);
                break;
            case ConsoleKey.A:
                UpdatePrompt("age", dog);
                break;
            case ConsoleKey.B:
                UpdatePrompt("breed", dog);
                break;
            default:
                Prompt.ShowInvalidMessage();
                UpdateDog(dog);
                break;
        }
    }

    static void UpdatePrompt(string field, Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"Enter updated {field}:");
        var updatedField = Console.ReadLine();
        switch (field)
        {
            case "name":
                dog.Name = updatedField;
                break;
            case "age":
                try
                {
                    var ageToInt = Int32.Parse(updatedField);
                    dog.Age = ageToInt;
                }
                catch (Exception e)
                {
                    Prompt.ShowInvalidMessage();
                    UpdatePrompt(field, dog);
                } 
                break;
            case "breed":
                dog.Breed = updatedField;
                break;
        }
        Console.Clear();
        Console.WriteLine($"Success! We've update {dog.Name}'s information");
        Thread.Sleep(1500);
        ShowDog(dog.Name);
    }
    
    public static void AddDog()
    {
        Console.Clear();
        Console.WriteLine("Great! Let's add a new dog!" +
                          "\n=========================");
        
        Thread.Sleep(2000);
        Console.Clear();
        
        var newDog = CreateDog();
        Prompt.Dogs.Add(newDog);
        Thread.Sleep(1500);
        ShowPromptToAddAnotherDog();
    }

    private static void ShowPromptToAddAnotherDog()
    {
        Console.WriteLine("Add another dog? Choose: yes/no");
        var userChoice = Console.ReadLine();
        switch (userChoice)
        {
            case "yes":
                AddDog();
                break;
            case "no":
                Prompt.ShowMainMenu();
                break;
            default:
                Prompt.ShowInvalidMessage();
                ShowPromptToAddAnotherDog();
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

    static void DeleteDog(Dog dog)
    {
        Console.Clear();
        Console.WriteLine($"Are you sure you want to delete {dog.Name}" +
                          $"\n(y)es/(n)o");
        var userInput = Console.ReadKey();
        switch (userInput.Key)
        {
            case ConsoleKey.Y:
                Console.Clear();
                Prompt.Dogs.Remove(dog);
                var owner = dog.Owner;
                try
                {
                    owner.Dogs.Remove(dog);
                }
                catch (NullReferenceException e)
                {
                    
                }
                Console.WriteLine($"Success! We have deleted {dog.Name} from our database.");
                Thread.Sleep(1000);
                break;
        }
        Thread.Sleep(1000);
        ShowDogs();
    }
}