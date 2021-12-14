using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PuppyPlace.Domain;

namespace PuppyPlace.Data;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        // builder.HasKey(nameof(PersonPersistence.Id));
        builder.HasKey(m => m.Id);
        builder.OwnsOne(m => m.Name)
            .Property(x => x.Value)
            .HasColumnName("Name");
    }
}

public class PersonPersistence
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static implicit operator Person(PersonPersistence persistence) =>
        new Person(persistence.Name);
}