using Microsoft.EntityFrameworkCore;
using PuppyPlace.Domain;

namespace PuppyPlace.Data;

public class PuppyPlaceContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Dog> Dogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasMany(m => m.Dogs).WithOne(m => m.Owner).OnDelete(DeleteBehavior.Cascade);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=/Users/aaroncadrian/Repos/PuppyPlace/PuppyPlace.Data/datuh.sqlite");
    }
}