namespace PuppyPlace.Api.Dtos;

public class GetDogDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }
}