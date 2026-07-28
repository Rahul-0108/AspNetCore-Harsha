using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
  public class PersonsDbContext : DbContext
  {
    public PersonsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Person> Persons { get; set; }

// Here we have the predefined object called ModelBuilder. You can configure the tables and corresponding indexes or primary keys or relations
//or any seed data. Everything can be configured by using the same ModelBuilder object.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Country>().ToTable("Countries"); // map to table
      modelBuilder.Entity<Person>().ToTable("Persons");

      //Seed to Countries
      string countriesJson = System.IO.File.ReadAllText("countries.json");
      List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

      foreach (Country country in countries)
        modelBuilder.Entity<Country>().HasData(country);


      //Seed to Persons
      string personsJson = System.IO.File.ReadAllText("persons.json");
      List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);

      foreach (Person person in persons)
        modelBuilder.Entity<Person>().HasData(person);
    }
  }
}
