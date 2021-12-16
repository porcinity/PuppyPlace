using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PuppyPlace.Domain.Value_Objects.DogValueObjects;

namespace PuppyPlace.Domain;

public class Dog
{
    public Guid Id { get; set; }

    private readonly DogName _name;
    public string Name => _name.Value;

    private readonly DogAge _age;
    public int Age => _age.Value;
    
    [Required]
    public string Breed { get; set; }
    [Column]
    public Person? Owner { get; set; }
    public Guid? OwnerId { get; set; }
    public Dog(DogName name, DogAge age, string breed)
    {
        Id = Guid.NewGuid();
        _name = name;
        _age = age;
        Breed = breed;
    }
    public Dog(){}
    public void AddOwner(Person owner)
    {
        Owner = owner;
    }
}