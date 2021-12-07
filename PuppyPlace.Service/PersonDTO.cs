using System.ComponentModel.DataAnnotations;

namespace PuppyPlace.Service;

public class PersonDTO
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
}