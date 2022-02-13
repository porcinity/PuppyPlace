using LanguageExt;
using LanguageExt.Common;
using LanguageExt.SomeHelp;
using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.DogValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Dogs.Commands;

public class PutDogCommand : IRequest<Option<Validation<Error, Dog>>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }
}

public class PutDogCommandHandler : IRequestHandler<PutDogCommand, Option<Validation<Error, Dog>>>
{
    private readonly IDogsRepository _dogsRepository;

    public PutDogCommandHandler(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }

    public async Task<Option<Validation<Error, Dog>>> Handle(PutDogCommand request, CancellationToken cancellationToken)
    {
        var dog = await _dogsRepository.FindDog(request.Id);


        var optDog = dog.ToSome().ToOption();

        var newName = DogName.Create(request.Name);
        var newAge = DogAge.Create(request.Age);
        var newBreed = DogBreed.Create(request.Breed);

        return optDog
            .Map(d =>
                (newName, newAge, newBreed)
                .Apply((n, a, b) =>
                    {
                        d.Update(n, a, b);
                        _dogsRepository.UpdateDog(d).Wait();
                        return d;
                    }));

        // var updatedDog = dog.Map(d =>
        //     (newName, newAge, newBreed)
        //     .Apply((name, age, breed) =>
        //         d.Update(name, age, breed)));

        // updatedDog
        //     .Succ(async d => await _dogsRepository.UpdateDog(dog))
        //     .Fail(e => {
        //         return e
        //             .Select(e => e.Message)
        //             .ToList()
        //             .AsTask();
        //     });
        //
        // return updatedDog;
    }
}