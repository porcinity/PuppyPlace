using MediatR;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Commands;

public class PutPersonCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class PutPersonCommandHandler : IRequestHandler<PutPersonCommand, Unit>
{
    private readonly IPersonsRepository _personsRepository;

    public PutPersonCommandHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }
    public async Task<Unit> Handle(PutPersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personsRepository.FindPerson(request.Id);
        person.Name = request.Name;
        await _personsRepository.UpdatePerson(person);
        return Unit.Value;
    }
} 