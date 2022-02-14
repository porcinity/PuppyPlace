using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using PuppyPlace.Repository;
using Unit = LanguageExt.Unit;

namespace PuppyPlace.Services.Persons.Commands;

public class AdoptDogCommand : IRequest<Option<Unit>>
{
    public Guid PersonId { get; set; }
    public Guid DogId { get; set; }

    public static AdoptDogCommand Create(Guid personId, Guid dogId)
    {
        var command = new AdoptDogCommand();
        command.PersonId = personId;
        command.DogId = dogId;
        return command;
    }
}

public class AdoptDogCommandHandler : IRequestHandler<AdoptDogCommand, Option<Unit>>
{
    private readonly IAdoptionService _adoptionService;

    public AdoptDogCommandHandler(IAdoptionService adoptionService)
    {
        _adoptionService = adoptionService;
    }
    public async Task<Option<Unit>> Handle(AdoptDogCommand request, CancellationToken cancellationToken)
    {
        return await _adoptionService.AdoptDog(request.PersonId, request.DogId);
    }
}