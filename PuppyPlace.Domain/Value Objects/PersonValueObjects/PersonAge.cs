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

    public static Validation<Error, PersonAge> Create(int value)
    {

        if (value < 15)
            return Fail<Error, PersonAge>("Age must be older than 15.");

        if (value > 90)
            return Fail<Error, PersonAge>("Age must be less than 90");

        return Success<Error, PersonAge>(new PersonAge(value));
    }
}