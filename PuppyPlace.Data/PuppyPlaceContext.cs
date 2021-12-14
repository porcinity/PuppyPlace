using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PuppyPlace.Domain;

namespace PuppyPlace.Data;

public class PuppyPlaceContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Dog> Dogs { get; set; }

    public DbSet<Client> Clients { get; set; }

    public PuppyPlaceContext(DbContextOptions<PuppyPlaceContext> options) : base(options)
    {

    }
    public PuppyPlaceContext()
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PersonConfiguration());

        builder.Entity<Client>().OwnsOne(m => m.Name).Property(m => m.Value).HasColumnName("Name");
    }

    public class PostgresContext : PuppyPlaceContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(@"Host=localhost;Username=test;Password=test;Database=TheNewPuppyPlace");
    }
}