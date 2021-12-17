using MediatR;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Commands;

public class AdoptDogCommand : IRequest<Unit>
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

public class AdoptDogCommandHandler : IRequestHandler<AdoptDogCommand, Unit>
{
    private readonly IAdoptionService _adoptionService;

    public AdoptDogCommandHandler(IAdoptionService adoptionService)
    {
        _adoptionService = adoptionService;
    }
    public async Task<Unit> Handle(AdoptDogCommand request, CancellationToken cancellationToken)
    {
        await _adoptionService.AdoptDog(request.PersonId, request.DogId);
        return Unit.Value;
    }
}