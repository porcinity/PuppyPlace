using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PuppyPlace.Domain;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Queries;

public class GetPersonByIdQuery : IRequest<Option<Person>>
{
    public Guid Id { get; set; }
}

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Option<Person>>
{
    private readonly IPersonsRepository _personsRepository;

    public GetPersonByIdQueryHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }

    public async Task<Option<Person>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        return await _personsRepository.FindPerson(request.Id);
    }
}