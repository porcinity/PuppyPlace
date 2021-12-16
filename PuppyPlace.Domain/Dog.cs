using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PuppyPlace.Domain.Value_Objects.DogValueObjects;

namespace PuppyPlace.Domain;

public class Dog
{
    public Guid Id { get; set; }

    private readonly DogName _name;
    public string Name => _name.Value;
    
    [Required]
    public int Age { get; set; }
    [Required]
    public string Breed { get; set; }
    [Column]
    public Person? Owner { get; set; }
    public Guid? OwnerId { get; set; }
    public Dog(DogName name, int age, string breed)
    {
        Id = Guid.NewGuid();
        _name = name;
        Age = age;
        Breed = breed;
    }
    public void AddOwner(Person owner)
    {
        Owner = owner;
    }
}