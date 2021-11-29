using Microsoft.EntityFrameworkCore;
using PuppyPlace.Domain;

namespace PuppyPlace.Data;

public class PuppyPlaceContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Dog> Dogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=./datuh.sqlite");
    }
}