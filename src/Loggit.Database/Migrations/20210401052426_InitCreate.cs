using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loggit.Database.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Landings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AirportName = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PilotName = table.Column<string>(type: "TEXT", nullable: false),
                    AircraftName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Takeoffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AirportName = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PilotName = table.Column<string>(type: "TEXT", nullable: false),
                    AircraftName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takeoffs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Landings");

            migrationBuilder.DropTable(
                name: "Takeoffs");
        }
    }
}
