using PuppyPlace.Domain.Value_Objects.PersonValueObjects;

namespace PuppyPlace.Domain;

public class Person
{
    public Guid Id { get; }
    
    private PersonName _name;
    public string Name => _name.Value;
    
    public PersonAge Age { get; private set; }
    
    private readonly List<Dog> _dogs;
    public IEnumerable<Dog> Dogs => _dogs;
    
    public Person(PersonName name, PersonAge age)
    {
        Id = Guid.NewGuid();
        _name = name;
        Age = age;
        _dogs = new List<Dog>();
    }

    private Person()
    {
    }

    public Person Update(PersonName name, PersonAge age)
    {
        _name = name;
        Age = age;
        return this;
    }

    public Person AddDog(Dog dog)
    {
        _dogs.Add(dog);
        return this;
    }
}