using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

namespace PuppyPlace.Domain;

public class Person
{
    [Key]
    public Guid Id { get; set; }

    [Required] public PersonName Name;

    [Column] private readonly List<Dog> _dogs;
    public Person(PersonName name)
    {
        Name = name;
        Id = Guid.NewGuid();
        _dogs = new List<Dog>();
    }

    public Person()
    {
    }

    // public string Name => _name.Value;
    public IEnumerable<Dog> Dogs => _dogs;

    public void AddDog(Dog dog)
    {
        _dogs.Add(dog);
    }
}