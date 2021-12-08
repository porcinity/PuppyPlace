using System.ComponentModel.DataAnnotations;
using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public class PersonDto
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }

    public IEnumerable<Dog> Dogs { get; set; } = new List<Dog>();
}