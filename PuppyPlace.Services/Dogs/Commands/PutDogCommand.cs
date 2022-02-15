using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;
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

        var newName = DogName.Create(request.Name);
        var newAge = DogAge.Create(request.Age);
        var newBreed = DogBreed.Create(request.Breed);

        var dogMapT = await _dogsRepository
            .FindDog(request.Id)
            .MapT(x =>
                (newName, newAge, newBreed)
                .Apply(x.Update));

         ignore(dogMapT.MapT(async d => await _dogsRepository.UpdateDog(d)));

        var updatedDog = dog
            .Map(d =>
                (newName, newAge, newBreed)
                .Apply(d.Update));

        ignore(updatedDog.MapT(async d => await _dogsRepository.UpdateDog(d)));

        return dogMapT;
    }
}