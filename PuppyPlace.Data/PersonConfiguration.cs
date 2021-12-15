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
        builder.OwnsOne<PersonAge>("_age")
            .Property(a => a.Value)
            .HasColumnName("Age")
            .IsRequired();
    }
}