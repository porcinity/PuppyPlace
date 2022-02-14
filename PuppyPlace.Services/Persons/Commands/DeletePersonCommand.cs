using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using PuppyPlace.Repository;
using Unit = LanguageExt.Unit;

namespace PuppyPlace.Services.Persons.Commands;

public class DeletePersonCommand : IRequest<Option<Unit>>
{
    public Guid Id { get; set; }
}

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Option<Unit>>
{
    private readonly IPersonsRepository _personsRepository;

    public DeletePersonCommandHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }
    public async Task<Option<Unit>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personsRepository.FindPerson(request.Id);
        ignore(person.Map(async x => await _personsRepository.DeletePerson(x)));

        return person.Map(x => unit);
    }
}