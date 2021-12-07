using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public interface IDogsService
{
    Task<IEnumerable<DogDto>> FindDogs();
    Task<DogDto> FindDog(Guid id);
    Task AddDog(DogDto dogDto);
    Task UpdateDog(DogDto dogDto);
    Task DeleteDog(Guid id);
}