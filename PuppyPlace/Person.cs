namespace PuppyPlace;

public class Person
{
    public string Name { get; set; }
    public Guid Id { get; set; }
    public List<Dog> Dogs { get; set; }
    public Person(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
        Dogs = new List<Dog>();
    }

    public void AddDog(Dog dog)
    {
        Dogs.Add(dog);
    }
}