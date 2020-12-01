using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Petthy.Data.Migrations
{
    public partial class DataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Professional_FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Professional_LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(nullable: false),
                    ProfessionalId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                });

            migrationBuilder.CreateTable(
                name: "PetAssignments",
                columns: table => new
                {
                    PetId = table.Column<int>(nullable: false),
                    ProfessionalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetAssignments", x => new { x.PetId, x.ProfessionalId });
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetName = table.Column<string>(nullable: true),
                    AnimalKind = table.Column<string>(nullable: true),
                    PetSex = table.Column<string>(nullable: true),
                    PetAge = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalAppointments",
                columns: table => new
                {
                    PetId = table.Column<int>(nullable: false),
                    ProfessionalId = table.Column<int>(nullable: false),
                    AppointmentDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalAppointments", x => new { x.ProfessionalId, x.PetId, x.AppointmentDateTime });
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalRoles",
                columns: table => new
                {
                    ProfessionalRoleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    professionalRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalRoles", x => x.ProfessionalRoleId);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalSchedules",
                columns: table => new
                {
                    ProfessionalId = table.Column<int>(nullable: false),
                    Weekday = table.Column<string>(nullable: false),
                    DateTimeBegin = table.Column<DateTime>(nullable: false),
                    DateTimeEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalSchedules", x => new { x.ProfessionalId, x.Weekday, x.DateTimeBegin, x.DateTimeEnd });
                });

            migrationBuilder.CreateTable(
                name: "PetDiaryNotes",
                columns: table => new
                {
                    PetDiaryNoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(nullable: false),
                    LearntCommands = table.Column<string>(nullable: true),
                    Advice = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    NoteDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetDiaryNotes", x => x.PetDiaryNoteId);
                    table.ForeignKey(
                        name: "FK_PetDiaryNotes_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetMedCardNotes",
                columns: table => new
                {
                    PetMedCardNoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(nullable: false),
                    Illness = table.Column<string>(nullable: true),
                    Treatment = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    NoteDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetMedCardNotes", x => x.PetMedCardNoteId);
                    table.ForeignKey(
                        name: "FK_PetMedCardNotes_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetDiaryNotes_PetId",
                table: "PetDiaryNotes",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_PetMedCardNotes_PetId",
                table: "PetMedCardNotes",
                column: "PetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "PetAssignments");

            migrationBuilder.DropTable(
                name: "PetDiaryNotes");

            migrationBuilder.DropTable(
                name: "PetMedCardNotes");

            migrationBuilder.DropTable(
                name: "ProfessionalAppointments");

            migrationBuilder.DropTable(
                name: "ProfessionalRoles");

            migrationBuilder.DropTable(
                name: "ProfessionalSchedules");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Professional_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Professional_LastName",
                table: "AspNetUsers");
        }
    }
}
