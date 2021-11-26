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
    
    // public void ShowDogs()
    // { 
    //     Console.WriteLine($"{Name} has the following dogs:");
    //         
    //     foreach (var dog in Dogs)
    //     {
    //         Console.WriteLine($"{dog.Name}");
    //     }
    // }
    
    //
    
}