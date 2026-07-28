using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Country>().ToTable("Countries");
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


      //Fluent API
      modelBuilder.Entity<Person>().Property(temp => temp.TIN)
        .HasColumnName("TaxIdentificationNumber")
        .HasColumnType("varchar(8)")
        .HasDefaultValue("ABC12345");

      //modelBuilder.Entity<Person>()
      //  .HasIndex(temp => temp.TIN).IsUnique();

      modelBuilder.Entity<Person>()
        .HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8");

      //Table Relations (not required)
      modelBuilder.Entity<Person>(entity =>
      {
        entity.HasOne<Country>(c => c.Country)
        .WithMany(p => p.Persons)
        .HasForeignKey(p => p.CountryID);
      });
      // What is the foreign key here? That is, CountryId property of the Person. So this will explicitly configure the foreign key, that is,
      // the CountryId property in the PersonModel class. But in general, it is not required to explicitly mention the relationship in 
      // the DbContext. So you can ignore the same. It is not required. It is the common practice that you will configure the same here itself. 
      // So just above the navigation property, you can write the ForeignKey attribute with CountryId. 
      // That's it. While creating the navigation property, you can mention the foreign key, and it will take up the relationship automatically. 
      // It internally applies the joins to load the corresponding related data from the other table.
    }

    public List<Person> sp_GetAllPersons()
    {
      return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
    }

    public int sp_InsertPerson(Person person)
    {
      SqlParameter[] parameters = new SqlParameter[] { 
        new SqlParameter("@PersonID", person.PersonID),
        new SqlParameter("@PersonName", person.PersonName),
        new SqlParameter("@Email", person.Email),
        new SqlParameter("@DateOfBirth", person.DateOfBirth),
        new SqlParameter("@Gender", person.Gender),
        new SqlParameter("@CountryID", person.CountryID),
        new SqlParameter("@Address", person.Address),
        new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters)
      };

      return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @PersonID, @PersonName, @Email, @DateOfBirth, @Gender, @CountryID, @Address, @ReceiveNewsLetters", parameters);
    }
  }
}
