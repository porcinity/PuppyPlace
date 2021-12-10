using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Repository;
using PuppyPlace.Service;

namespace PuppyPlace.Api.Queries;

public static class GetAllPersons
{
    public record Query() : IRequest<Response>;

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IPersonsRepository _personsRepository;

        public Handler(IPersonsRepository personsRepository)
        {
            _personsRepository = personsRepository;
        }
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var persons = await _personsRepository.FindPersons();
            return new Response(persons);
        }
    }

    public record Response(IEnumerable<Person> Persons);
}