using System.ComponentModel.DataAnnotations;
using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public class PersonDTO
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<Dog> Dogs { get; set; } = new List<Dog>();
}