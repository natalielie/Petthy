using Microsoft.EntityFrameworkCore.Migrations;

namespace Petthy.Data.Migrations
{
    public partial class DataMigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "AnimalKind", "ClientId", "PetAge", "PetName", "PetSex" },
                values: new object[] { 1, "Cat", 1, 1, "Twinkie", "Female" });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "AnimalKind", "ClientId", "PetAge", "PetName", "PetSex" },
                values: new object[] { 2, "Dog", 2, 3, "Jim", "Male" });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "AnimalKind", "ClientId", "PetAge", "PetName", "PetSex" },
                values: new object[] { 3, "Cat", 2, 1, "Cinnabon", "Male" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "PetId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "PetId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "PetId",
                keyValue: 3);
        }
    }
}
