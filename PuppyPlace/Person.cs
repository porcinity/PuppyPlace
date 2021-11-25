namespace PuppyPlace;

public class Person
{
    public string Name { get; set; }
    public Guid Id { get; set; }
    public List<Dog> Dogs { get; set; }

    public Person(string name)
    {
        var rnd = new Random();
            
        Name = name;
        Id = Guid.NewGuid();
        Dogs = new List<Dog>();
    }

    public void AddDog(Dog dog)
    {
        Dogs.Add(dog);
    }

    public void ShowDogs()
    { 
        Console.WriteLine($"{Name} has the following dogs:");
            
        foreach (var dog in Dogs)
        {
            Console.WriteLine($"{dog.Name}");
        }
    }
    
    //
    public static void AddPerson()
    {
        Console.Clear();
        Console.WriteLine("Great! Let's add a new person!" +
                          "\n==============================");
        Thread.Sleep(2000);
        Console.Clear();

        var newPerson = CreatePerson();
        
        Prompt.Persons.Add(newPerson);
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
                Prompt.ShowMainMenu();
                break;
            default:
                Console.Clear();
                AddAnotherPerson();
                break;
        }
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

    public static void ShowPeople()
    {
        Console.Clear();
        Console.WriteLine("These are the people in our database:");

        var num = 1;
        foreach (var person in Prompt.Persons)
        {
            Console.WriteLine($"{num} - {person.Name}");
            num++;
        }
        
        Console.WriteLine("Press (m) to return to main menu.");
        var userPress = Console.ReadKey(true);

        switch (userPress.Key)
        {
            case ConsoleKey.M:
                Prompt.ShowMainMenu();
                break;
            default:
                ShowPeople();
                break;
        }
    }
}