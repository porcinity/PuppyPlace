namespace PuppyPlace;

public class Dog
{
    private Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }
    public Person? Owner { get; set; }
    public Dog(string name, int age, string breed)
    {
        Id = Guid.NewGuid();
        Name = name;
        Age = age;
        Breed = breed;
    }
    public void AddOwner(Person owner)
    {
        Owner = owner;
    }
}