using LanguageExt;
using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Repository;

namespace PuppyPlace.Services.Dogs.Queries;

public class GetDogByIdQuery : IRequest<Option<Dog>>
{
    public Guid Id { get; set; }
}

public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Option<Dog>>
{
    private readonly IDogsRepository _dogsRepository;

    public GetDogByIdQueryHandler(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }
    public async Task<Option<Dog>> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
    {
        return await _dogsRepository.FindDogWithOwner(request.Id);

    }
}