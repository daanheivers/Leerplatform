using Microsoft.EntityFrameworkCore.Migrations;

namespace Leerplatform.Migrations
{
    public partial class Aanvaardkolom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aanvaard",
                table: "Inschrijvingen",
                type: "Bit",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_UserId",
                table: "Inschrijvingen",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_VakId",
                table: "Inschrijvingen",
                column: "VakId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aanvaard",
                table: "Inschrijvingen");
        }
    }
}
