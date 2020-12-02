using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Petthy.Data.Migrations
{
    public partial class DataMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Professionals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Clients",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Address", "DateOfBirth", "FirstName", "LastName", "PhoneNumber", "UserId" },
                values: new object[] { 1, "Riverside st, 33b", new DateTime(1988, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tyler", "Joseph", "+40097656789", null });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Address", "DateOfBirth", "FirstName", "LastName", "PhoneNumber", "UserId" },
                values: new object[] { 2, "Riverside st, 33a", new DateTime(1988, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joshua", "Dun", "+40054776512", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Clients");
        }
    }
}
