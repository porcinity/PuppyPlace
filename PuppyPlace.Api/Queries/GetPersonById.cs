using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Repository;

namespace PuppyPlace.Api.Queries;

public static class GetPersonById
{
    public record Query(Guid id) : IRequest<Response>;

    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IPersonsRepository _personsRepository;

        public Handler(IPersonsRepository personsRepository)
        {
            _personsRepository = personsRepository;
        }
        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var person = await _personsRepository.FindPerson(request.id);
            return person == null ? null : new Response(person.Id, person.Name, person.Dogs);
        }
    }

    public record Response(Guid id, string name, IEnumerable<Dog> dogs);
}