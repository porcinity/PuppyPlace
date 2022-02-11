using LanguageExt;
using LanguageExt.Common;
using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.DogValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Dogs.Commands;

public class CreateDogCommand : IRequest<Validation<Error, Dog>>
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

public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, Validation<Error, Dog>>
{
    private readonly IDogsRepository _dogsRepository;

    public CreateDogCommandHandler(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }

    public async Task<Validation<Error, Dog>> Handle(CreateDogCommand request, CancellationToken cancellationToken)
    {
        var dogName = DogName.Create(request.Name);
        var dogAge = DogAge.Create(request.Age);
        var dogBreed = DogBreed.Create(request.Breed);

        var newDog =  from name in dogName
                                        from age in dogAge
                                        from breed in dogBreed
                                        select new Dog(name, age, breed);

        var dog = (dogName, dogAge, dogBreed)
            .Apply((n, a, b) => new Dog(n, a, b));

        dog
            .Succ(async d => { await _dogsRepository.AddDog(d); })
            .Fail(e => { return e.AsTask(); });

        return dog;
    }
}