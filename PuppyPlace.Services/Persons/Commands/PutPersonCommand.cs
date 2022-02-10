
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.PersonValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Commands;

public class PutPersonCommand : IRequest<Validation<Error, Person>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

public class PutPersonCommandHandler : IRequestHandler<PutPersonCommand, Validation<Error, Person>>
{
    private readonly IPersonsRepository _personsRepository;

    public PutPersonCommandHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }
    public async Task<Validation<Error, Person>> Handle(PutPersonCommand command, CancellationToken cancellationToken)
    {
        var person = await _personsRepository.FindPerson(command.Id);
        var name = PersonName.Create(command.Name);
        var age = PersonAge.Create(command.Age);

        var updatedPerson = (name, age).Apply(person.Update);

        updatedPerson
            .Succ(async p =>
            {
                await _personsRepository.UpdatePerson(p);
            })
            .Fail(e =>
            {
                return e.AsTask();
            });

        return updatedPerson;
    }
}