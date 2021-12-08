using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuppyPlace.Domain;

public class Dog
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(30,ErrorMessage = "Max length for name is 30 characters")]
    public string Name { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public string Breed { get; set; }
    [Column]
    public Person? Owner { get; set; }
    public Guid? OwnerId { get; set; }
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