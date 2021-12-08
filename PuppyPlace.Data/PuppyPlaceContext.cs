using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PuppyPlace.Domain;

namespace PuppyPlace.Data;

public class PuppyPlaceContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Dog> Dogs { get; set; }

    public PuppyPlaceContext(DbContextOptions<PuppyPlaceContext> options) : base(options)
    {
        
    }
    public PuppyPlaceContext()
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(@"Host=localhost;Username=test;Password=test;Database=PuppyPlace");
    }
}