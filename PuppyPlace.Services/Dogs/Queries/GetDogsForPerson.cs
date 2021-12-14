using MediatR;
using PuppyPlace.Domain;
using PuppyPlace.Service;

namespace PuppyPlace.Services.Dogs.Queries;

public class GetDogsForPersonInput : IRequest<GetDogsForPersonOutput>
{
    public Guid PersonId { get; init; }
}

public class GetDogsForPersonOutput
{
    public List<DogDto> Dogs { get; init; }
    
    public string? Cursor { get; init; }
}

public class GetDogsForPersonHandler : IRequestHandler<GetDogsForPersonInput,GetDogsForPersonOutput>
{
    public  async Task<GetDogsForPersonOutput> Handle(GetDogsForPersonInput request, CancellationToken cancellationToken)
    {
        // get dogs where ownerId = request.PersonId from repository
        
        // map dogs to DogDto list
        var mappedDogs = new List<DogDto>();

        return new GetDogsForPersonOutput()
        {
            Dogs = mappedDogs
        };
    }
}