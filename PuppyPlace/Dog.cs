namespace PuppyPlace;

public class Dog
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }
    public List<Person> Owner { get; set; }

    public Dog(string name, int age, string breed)
    {
        Name = name;
        Age = age;
        Breed = breed;
        Owner = new List<Person>();
    }
        
    public string Bark()
    {
        return "WOOF!!!";
    }

    public void AddOwner(Person owner)
    {
        Owner.Add(owner);
    }

    public void ShowOwners()
    {
        Console.WriteLine($"{Name} belongs to these people:");
        foreach (var owner in Owner)
        {
            Console.WriteLine($"{owner.Name}");
        }
    }

    public static void AddDog()
    {
        Console.WriteLine("Please insert the dog's name:");
        var newDogName = Console.ReadLine();
            
        Console.WriteLine($"Please insert {newDogName}'s age:");
        var newDogAge = Console.ReadLine();
            
        Console.WriteLine($"Please insert {newDogName}'s breed:");
        var newDogBreed = Console.ReadLine();

        var newDog = new Dog(newDogName, Int32.Parse(newDogAge), newDogBreed);
            
        Console.WriteLine("Success! We added the following information to the database:" +
                          "\n==========================================================" +
                          $"\nName: {newDog.Name}" +
                          $"\nAge: {newDogAge}" +
                          $"\nBreed: {newDogBreed}" +
                          $"\n========================================================="
        );
    }
}
