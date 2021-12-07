using PuppyPlace.Domain;

namespace PuppyPlace.Service;

public interface IDogsService
{
    Task<IEnumerable<DogDTO>> FindDogs();
    Task<DogDTO> FindDog(Guid id);
    Task AddDog(DogDTO dogDto);
    Task UpdateDog(DogDTO dogDto);
    Task DeleteDog(Guid id);
}