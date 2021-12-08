using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuppyPlace.Domain;

public class Person
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }

    [Column] private readonly List<Dog> _dogs;

    public IEnumerable<Dog> Dogs => _dogs;
    public Person(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
        _dogs = new List<Dog>();
    }

    public void AddDog(Dog dog)
    {
        _dogs.Add(dog);
    }
}