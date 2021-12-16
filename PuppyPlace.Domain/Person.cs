using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

namespace PuppyPlace.Domain;

public class Person
{
    public Guid Id { get; }
    
    private readonly PersonName _name;
    public string Name => _name.Value;
    
    private readonly PersonAge _age;
    public int Age => _age.Value;
    
    private readonly List<Dog> _dogs;
    public IEnumerable<Dog> Dogs => _dogs;
    
    public Person(PersonName name, PersonAge age)
    {
        Id = Guid.NewGuid();
        _name = name;
        _age = age;
        _dogs = new List<Dog>();
    }

    private Person()
    {
    }


    public void AddDog(Dog dog)
    {
        _dogs.Add(dog);
    }
}