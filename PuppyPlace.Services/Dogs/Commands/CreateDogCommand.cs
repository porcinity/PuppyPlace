using MediatR;
using PuppyPlace.Domain;
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
        var dog = new Dog(request.Name, request.Age, request.Breed);
        await _dogsRepository.AddDog(dog);
        return dog.Id;
    }
}