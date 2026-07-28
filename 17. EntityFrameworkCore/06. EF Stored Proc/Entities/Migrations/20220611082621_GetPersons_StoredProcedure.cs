using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
  public partial class GetPersons_StoredProcedure : Migration
  {
  //Whenever you run the update database command next time, this up method executes. But in case in future when you want to roll back the migration, then this down method executes.
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      string sp_GetAllPersons = @"
        CREATE PROCEDURE [dbo].[GetAllPersons]
        AS BEGIN
          SELECT PersonID, PersonName, Email, DateOfBirth, Gender, CountryID, Address, ReceiveNewsLetters FROM [dbo].[Persons]
        END
      ";
      migrationBuilder.Sql(sp_GetAllPersons);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      string sp_GetAllPersons = @"
        DROP PROCEDURE [dbo].[GetAllPersons]
      ";
      migrationBuilder.Sql(sp_GetAllPersons);
    }
  }
}
