using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PuppyPlace.Domain;
using PuppyPlace.Domain.Value_Objects.DogValueObjects;

namespace PuppyPlace.Data;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.HasKey("Id");
        builder.OwnsOne<DogName>("_name")
            .Property(x => x.Value)
            .HasColumnName("Name")
            .IsRequired();
        builder.OwnsOne<DogAge>("_age")
            .Property(x => x.Value)
            .HasColumnName("Age")
            .IsRequired();
        builder.OwnsOne<DogBreed>("_breed")
            .Property(x => x.Value)
            .HasColumnName("Breed")
            .IsRequired();
    }
}