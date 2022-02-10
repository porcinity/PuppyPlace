using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace PuppyPlace.Domain.Value_Objects.PersonValueObjects;

public readonly record struct PersonAge
{
    public readonly int Value;

    private PersonAge(int value)
    {
        Value = value;
    }

    public static Validation<Error, PersonAge> Create(int value) =>
        value switch
        {
            < 16 => Fail<Error, PersonAge>("Age must be older than 16."),
            > 100 => Fail<Error, PersonAge>("Age must be less than 100"),
            _ => Success<Error, PersonAge>(new PersonAge(value))
        };
}