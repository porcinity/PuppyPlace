using PuppyPlace.Domain;

namespace PuppyPlace.Api.Dtos;

public class GetDogsDto
{
    private List<GetDogDto> _dogs { get; } = new List<GetDogDto>();
    public IEnumerable<GetDogDto> Dogs => _dogs;

    public static GetDogsDto Create(IEnumerable<Dog> dogs)
    {
        var dto = new GetDogsDto();
        foreach (var dog in dogs)
        {
            var dogDto = GetDogDto.Create(dog);
            dto._dogs.Add(dogDto);
        }

        return dto;
    }
}