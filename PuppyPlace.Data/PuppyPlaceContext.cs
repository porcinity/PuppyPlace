using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PuppyPlace.Domain;

namespace PuppyPlace.Data;

public class PuppyPlaceContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder
    //         .Entity<Person>()
    //         .HasMany(m => m.Dogs)
    //         .WithOne(m => m.Owner)
    //         .OnDelete(DeleteBehavior.Cascade);
    //
    //     modelBuilder
    //         .Entity<Dog>()
    //         .HasOne(m => m.Owner);
    // }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            // .UseSqlServer("Server=localhost,1433;Initial Catalog=helpme; User=sa; Password=Strong.Pwd-123")
            .UseNpgsql(@"Host=localhost;Username=test;Password=test;Database=PuppyPlace");
    }
}