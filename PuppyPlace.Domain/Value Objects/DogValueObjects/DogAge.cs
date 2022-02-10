using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace PuppyPlace.Domain.Value_Objects.DogValueObjects;

public record DogAge
{
    public int Value { get; }

    private DogAge(int value)
    {
        Value = value;
    }

    public static Validation<Error, DogAge> Create(int value) =>
        value switch
        {
            < 0 => Fail<Error, DogAge>("Age must be greater than 0."),
            > 20 => Fail<Error, DogAge>("Age must be less than 20"),
            _ => Success<Error, DogAge>(new DogAge(value))
        };
}