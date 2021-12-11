using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Dogs.Queries;

public record GetAllDogsQuery : IRequest<IEnumerable<Dog>>;

public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, IEnumerable<Dog>>
{
    private readonly IDogsRepository _dogsRepository;

    public GetAllDogsQueryHandler(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }
    public async Task<IEnumerable<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
    {
        return await _dogsRepository.FindDogs();
    }
}