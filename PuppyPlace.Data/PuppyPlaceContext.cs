using Microsoft.EntityFrameworkCore;
using PuppyPlace.Domain;

namespace PuppyPlace.Data;

public class PuppyPlaceContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Person>()
            .HasMany(m => m.Dogs)
            .WithOne(m => m.Owner)
            .OnDelete(DeleteBehavior.Cascade);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=helpme; User=sa; Password=Strong.Pwd-123");
    }
}