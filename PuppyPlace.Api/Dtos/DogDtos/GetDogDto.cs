namespace PuppyPlace.Api.Dtos;

public class GetDogDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }
    public DogOwnerDto Owner { get; set; }
}