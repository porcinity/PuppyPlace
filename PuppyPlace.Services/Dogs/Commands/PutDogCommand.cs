using MediatR;
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
        dog.Name = request.Name;
        dog.Age = request.Age;
        dog.Breed = request.Breed;
        await _dogsRepository.UpdateDog(dog);
        return dog.Id;
    }
}