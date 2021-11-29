using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuppyPlace.Domain;

public class Person
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Column]
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