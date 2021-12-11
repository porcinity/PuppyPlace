using MediatR;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Dogs.Commands;

public class DeleteDogCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, Unit>
{
    private readonly IDogsRepository _dogsRepository;

    public DeleteDogCommandHandler(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }
    public async Task<Unit> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
    {
        await _dogsRepository.DeleteDog(request.Id);
        return Unit.Value;
    }
}