using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace PuppyPlace.Domain.Value_Objects.DogValueObjects;

public record DogBreed
{
    public readonly string Value;

    private DogBreed(string value)
    {
        Value = value;
    }

    public static Validation<Error, DogBreed> Create(string value) =>
        value switch
        {
            var x when string.IsNullOrEmpty(x) =>
                Fail<Error, DogBreed>("Breed can't be null or empty."),

            var x when x.Length > 30 =>
                Fail<Error, DogBreed>("Breed can't be longer than 30 characters."),

            var x when Regex.IsMatch(x, @"\s") =>
                Fail<Error, DogBreed>("Breed can't contain whitespace."),

            var x when Regex.IsMatch(x, @"[0-9]") =>
                Fail<Error, DogBreed>("Breed can't contain numbers."),

            var x when x.Any(c => !char.IsLetter(c)) =>
                Fail<Error, DogBreed>("Breed cannot contain special chars."),

            _ => Success<Error, DogBreed>(new DogBreed(value))
        };
}