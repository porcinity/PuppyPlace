using MediatR;
using Microsoft.EntityFrameworkCore;
using PuppyPlace.Domain;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Persons.Queries;

public class GetAllPersonsQuery : IRequest<IEnumerable<Person>> { }
public class GetAllPersonsHandler : IRequestHandler<GetAllPersonsQuery, IEnumerable<Person>>
{
    private readonly IPersonsRepository _personsRepository;

    public GetAllPersonsHandler(IPersonsRepository personsRepository)
    {
        _personsRepository = personsRepository;
    }
    public async Task<IEnumerable<Person>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
       return  await _personsRepository.FindPersons();
    }
}