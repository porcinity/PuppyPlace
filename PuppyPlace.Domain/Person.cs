using System.ComponentModel.DataAnnotations;

namespace PuppyPlace.Domain;

public class Person
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; }

    public List<Dog> Dogs { get; set; } = new List<Dog>();
    public Person(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }

    public void AddDog(Dog dog)
    {
        Dogs.Add(dog);
    }
}