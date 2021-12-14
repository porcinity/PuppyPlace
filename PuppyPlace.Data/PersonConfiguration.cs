using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Ojbects.PersonValueObjects;

namespace PuppyPlace.Data;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        // builder.HasKey(nameof(PersonPersistence.Id));
        builder.HasKey(m => m.Id);
        builder.OwnsOne<PersonName>("_name")
            .Property(x => x.Value)
            .HasColumnName("Name")
            .IsRequired();
    }
}

public class PersonPersistence
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static implicit operator Person(PersonPersistence persistence) =>
        new Person(persistence.Name);
}