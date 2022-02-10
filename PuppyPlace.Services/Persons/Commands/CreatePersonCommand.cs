using LanguageExt;
using LanguageExt.Common;
using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.PersonValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Commands;

public class CreatePersonCommand : IRequest<Validation<Error, Person>>
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Validation<Error, Person>>
{
    private readonly IPersonsRepository _personsRepository;

    public CreatePersonCommandHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }

    public async Task<Validation<Error, Person>> Handle(CreatePersonCommand request,
        CancellationToken cancellationToken)
    {
        var name = PersonName.Create(request.Name);
        var age = PersonAge.Create(request.Age);

        var newPerson = (name, age).Apply((n, a) => new Person(n, a));

        newPerson
            .Succ(async p =>
            {
                await _personsRepository.AddPerson(p);
            })
            .Fail(e =>
            {
                return e.AsTask();
            });

        return newPerson;
    }
}