using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace PuppyPlace.Domain.Value_Objects.PersonValueObjects;

public record PersonName
{
    private readonly string _value;

    private PersonName(string value)
    {
        _value = value;
    }

    public string Value => _value;

    public static Validation<Error, PersonName> Create(string value) =>
        value switch
        {
            var x when string.IsNullOrEmpty(x) =>
                Fail<Error, PersonName>("Name cannot be empty."),
            var x when x.Length > 30 =>
                Fail<Error, PersonName>("Name cannot be longer than 30 characters."),
            var x when Regex.IsMatch(x, @"\s") =>
                Fail<Error, PersonName>("Name cannot contain white space."),
            var x when Regex.IsMatch(x, @"[0-9]") =>
                Fail<Error, PersonName>("Name cannot contain numbers."),
            var x when x.Any(c => !char.IsLetter(c)) =>
                Fail<Error, PersonName>("Name cannot contain special characters."),

            _ => Success<Error, PersonName>(new PersonName(value))
        };
}