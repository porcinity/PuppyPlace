using MediatR;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Commands;

public class DeletePersonCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
{
    private readonly IPersonsRepository _personsRepository;

    public DeletePersonCommandHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        await _personsRepository.DeletePerson(request.Id);
        return Unit.Value;
    }
}