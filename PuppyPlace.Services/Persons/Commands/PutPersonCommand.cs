using LanguageExt;
using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.PersonValueObjects;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Commands;

public class PutPersonCommand : IRequest<Option<Person>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

public class PutPersonCommandHandler : IRequestHandler<PutPersonCommand, Option<Person>>
{
    private readonly IPersonsRepository _personsRepository;

    public PutPersonCommandHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }
    public async Task<Option<Person>> Handle(PutPersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personsRepository.FindPerson(request.Id);
        var newName = new PersonName(request.Name);
        var newAge = PersonAge.Create(request.Age);

        newAge.MapAsync(async a =>
        {
            person.Update(newName, a);
            await _personsRepository.UpdatePerson(person);
        });

        return person;
    }
} 