using LanguageExt;
using LanguageExt.SomeHelp;
using static LanguageExt.Prelude;
using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.PersonValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Commands;

public class CreatePersonCommand : IRequest<Option<Person>>
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Option<Person>>
{
    private readonly IPersonsRepository _personsRepository;

    public CreatePersonCommandHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }

    public async Task<Option<Person>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var name = new PersonName(request.Name);
        var age = PersonAge.Create(request.Age);
        var person = from a in age
            select new Person(name, a);

        // Some p => good stuff
        // None => some other stuff
        // match(from a in age select a, Some: a => new Person(name, a).ToSome());

        // var result = person.Map(x =>
        // {
        //     _personsRepository.AddPerson(x);
        //     return x.ToSome();
        // });
        //
        // return result;

        // match(person,
        //     Some: p => p
        // );

        var result = person.MapAsync(async p =>

            await _personsRepository.AddPerson(p)
            );

        return person;

    }
}