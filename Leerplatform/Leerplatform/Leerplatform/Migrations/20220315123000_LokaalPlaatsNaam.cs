using Microsoft.EntityFrameworkCore.Migrations;

namespace Leerplatform.Migrations
{
    public partial class LokaalPlaatsNaam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Naam",
                table: "Middelen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Naam",
                table: "Lokalen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plaats",
                table: "Lokalen",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Naam",
                table: "Middelen");

            migrationBuilder.DropColumn(
                name: "Naam",
                table: "Lokalen");

            migrationBuilder.DropColumn(
                name: "Plaats",
                table: "Lokalen");
        }
    }
}
