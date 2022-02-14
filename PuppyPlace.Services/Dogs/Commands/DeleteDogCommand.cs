using LanguageExt;
using static LanguageExt.Prelude;
using MediatR;
using PuppyPlace.Repository;
using Unit = LanguageExt.Unit;

namespace PuppyPlace.Services.Dogs.Commands;

public class DeleteDogCommand : IRequest<Option<Unit>>
{
    public Guid Id { get; set; }
}

public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, Option<Unit>>
{
    private readonly IDogsRepository _dogsRepository;

    public DeleteDogCommandHandler(IDogsRepository dogsRepository)
    {
        _dogsRepository = dogsRepository;
    }
    public async Task<Option<Unit>> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
    {
        var dog = await _dogsRepository.FindDog(request.Id);

        ignore(dog.Map(async d => await _dogsRepository.DeleteDog(d)));

        var result = dog.Map(d => unit);
        return result;
    }
}