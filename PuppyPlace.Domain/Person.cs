using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

namespace PuppyPlace.Domain;

public class Person
{
    [Key] public Guid Id { get; set; }

    private readonly PersonName _name;
    public string Name => _name.Value;


    [Column] private readonly List<Dog> _dogs;
    public Person(PersonName name)
    {
        _name = name;
        Id = Guid.NewGuid();
        _dogs = new List<Dog>();
    }

    private Person()
    {
    }

    public IEnumerable<Dog> Dogs => _dogs;

    public void AddDog(Dog dog)
    {
        _dogs.Add(dog);
    }
}