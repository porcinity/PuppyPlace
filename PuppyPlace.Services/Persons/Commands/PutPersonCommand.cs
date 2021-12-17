using MediatR;
using PuppyPlace.Domain.Value_Objects.PersonValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Commands;

public class PutPersonCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
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
        var newName = new PersonName(request.Name);
        var newAge = new PersonAge(request.Age);
        person.Update(newName, newAge);
        await _personsRepository.UpdatePerson(person);
        return Unit.Value;
    }
} 