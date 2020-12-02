using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Petthy.Data.Migrations
{
    public partial class DataMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartDeviceData",
                columns: table => new
                {
                    SmartDeviceDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(nullable: false),
                    IsIll = table.Column<bool>(nullable: false),
                    IsEnoughWalking = table.Column<bool>(nullable: false),
                    SmartDeviceDataDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartDeviceData", x => x.SmartDeviceDataId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartDeviceData");
        }
    }
}
