using MediatR;
using PuppyPlace.Repository;
using PuppyPlace.Service;

namespace PuppyPlace.Services.Persons.Commands;

public class AdoptDogCommand : IRequest<Unit>
{
    public Guid PersonId { get; set; }
    public Guid DogId { get; set; }
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