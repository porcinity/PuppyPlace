using MediatR;
using PuppyPlace.Domain.Value_Objects.DogValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Dogs.Commands;

public class PutDogCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }
}

public class PutDogCommandHandler : IRequestHandler<PutDogCommand, Guid>
{
    private readonly IDogsRepository _dogsRepository;

    public PutDogCommandHandler(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }
    public async Task<Guid> Handle(PutDogCommand request, CancellationToken cancellationToken)
    {
        var dog = await _dogsRepository.FindDog(request.Id);
        var newName = DogName.Create(request.Name);
        var newAge = DogAge.Create(request.Age);
        var newBreed = DogBreed.Create(request.Breed);
        dog.Update(newName, newAge, newBreed);
        await _dogsRepository.UpdateDog(dog);
        return dog.Id;
    }
}