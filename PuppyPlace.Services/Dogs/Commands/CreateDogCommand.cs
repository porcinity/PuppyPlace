using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.DogValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Dogs.Commands;

public class CreateDogCommand : IRequest<Dog>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }

    public CreateDogCommand(string name, int age, string breed)
    {
        Name = name;
        Age = age;
        Breed = breed;
    }
}

public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, Dog>
{
    private readonly IDogsRepository _dogsRepository;

    public CreateDogCommandHandler(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }
    public async Task<Dog> Handle(CreateDogCommand request, CancellationToken cancellationToken)
    {
        var dogName = DogName.Create(request.Name);
        var dogAge = DogAge.Create(request.Age);
        var dogBreed = DogBreed.Create(request.Breed);
        var dog = new Dog(dogName, dogAge, dogBreed);
        await _dogsRepository.AddDog(dog);
        return dog;
    }
}