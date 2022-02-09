using LanguageExt;
using LanguageExt.SomeHelp;

namespace PuppyPlace.Domain.Value_Objects.PersonValueObjects;

public readonly record struct PersonAge
{
    public readonly int Value;
    private PersonAge(int value)
    {
        Value = value;
    }
    public static Option<PersonAge> Create(int value)
    {
        if (value < 1)
        {
            // throw new Exception("Too young.");
            return Option<PersonAge>.None;
        }
        if (value > 100)
        {
            // throw new Exception("Too old.");
            return Option<PersonAge>.None;
        }

        return new PersonAge(value).ToSome();
    }
}