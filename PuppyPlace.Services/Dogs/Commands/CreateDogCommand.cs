using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.DogValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Dogs.Commands;

public class CreateDogCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }
}

public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, Guid>
{
    private readonly IDogsRepository _dogsRepository;

    public CreateDogCommandHandler(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }
    public async Task<Guid> Handle(CreateDogCommand request, CancellationToken cancellationToken)
    {
        var dogName = DogName.Create(request.Name);
        var dogAge = DogAge.Create(request.Age);
        var dog = new Dog(dogName, dogAge, request.Breed);
        await _dogsRepository.AddDog(dog);
        return dog.Id;
    }
}