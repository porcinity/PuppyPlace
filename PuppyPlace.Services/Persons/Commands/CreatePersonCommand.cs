using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.PersonValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Commands;

public class CreatePersonCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
{
    private readonly IPersonsRepository _personsRepository;

    public CreatePersonCommandHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }

    public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var name = new PersonName(request.Name);
        var age = PersonAge.Create(request.Age);
        var person = new Person(name, age);
        await _personsRepository.AddPerson(person);
        return person.Id;
    }
}

