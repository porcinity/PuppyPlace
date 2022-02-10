using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace PuppyPlace.Domain.Value_Objects.DogValueObjects;

public record DogName
{
    public readonly string Value;

    private DogName(string value)
    {
        Value = value;
    }

    public static Validation<Error, DogName> Create(string value) =>
        value switch
        {
            var x when string.IsNullOrEmpty(x) =>
                Fail<Error, DogName>("Name can't be null or empty."),

            var x when x.Length > 30 =>
                Fail<Error, DogName>("Name can't be longer than 30 characters."),

            var x when Regex.IsMatch(x, @"\s") =>
                Fail<Error, DogName>("Name can't contain whitespace."),

            var x when Regex.IsMatch(x, @"[0-9]") =>
                Fail<Error, DogName>("Name can't contain numbers."),

            var x when x.Any(c => !char.IsLetter(c)) =>
                Fail<Error, DogName>("Name cannot contain special chars."),

            _ => Success<Error, DogName>(new DogName(value))
        };
}