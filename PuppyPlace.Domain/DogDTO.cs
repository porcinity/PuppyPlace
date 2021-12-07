using System.ComponentModel.DataAnnotations;

namespace PuppyPlace.Domain;

public class DogDTO
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public string Breed { get; set; }
    public Guid? OwnerId { get; set; }
}