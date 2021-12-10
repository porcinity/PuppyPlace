using MediatR;
using Microsoft.EntityFrameworkCore;
using PuppyPlace.Domain;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Queries;

public class GetPersonByIdQuery : IRequest<Person>
{
    public Guid Id { get; set; }
}

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Person>
{
    private readonly IPersonsRepository _personsRepository;

    public GetPersonByIdQueryHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }

    public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _personsRepository.FindPerson(request.Id);
        return person;
    }
}