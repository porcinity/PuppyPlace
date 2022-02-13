using LanguageExt;
using static LanguageExt.Prelude;
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

        var updatedDog = optDog
            .Map(d =>
                (newName, newAge, newBreed)
                .Apply(d.Update));

        ignore(
            updatedDog
                .Map(d =>
                    d.Map(async x => await _dogsRepository.UpdateDog(x)))
        );

        return updatedDog;
    }
}