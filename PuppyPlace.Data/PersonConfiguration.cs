using LanguageExt.UnsafeValueAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.PersonValueObjects;

namespace PuppyPlace.Data;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey("Id");
        builder.OwnsOne<PersonName>("_name")
            .Property(x => x.Value)
            .HasColumnName("Name")
            .IsRequired();
        builder.Property(p => p.Age)
            .HasConversion(p => p.Value,
                value => PersonAge.Create(value).ToEither().Value())
            .IsRequired();
    }
}