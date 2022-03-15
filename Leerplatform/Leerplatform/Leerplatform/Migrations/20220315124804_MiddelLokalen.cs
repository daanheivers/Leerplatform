using Microsoft.EntityFrameworkCore.Migrations;

namespace Leerplatform.Migrations
{
    public partial class MiddelLokalen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Middelen_Lokalen_LokaalId",
                table: "Middelen");

            migrationBuilder.DropIndex(
                name: "IX_Middelen_LokaalId",
                table: "Middelen");

            migrationBuilder.DropColumn(
                name: "LokaalId",
                table: "Middelen");

            migrationBuilder.CreateTable(
                name: "LokaalMiddel",
                columns: table => new
                {
                    LokalenLokaalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MiddelenMiddelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LokaalMiddel", x => new { x.LokalenLokaalId, x.MiddelenMiddelId });
                    table.ForeignKey(
                        name: "FK_LokaalMiddel_Lokalen_LokalenLokaalId",
                        column: x => x.LokalenLokaalId,
                        principalTable: "Lokalen",
                        principalColumn: "LokaalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LokaalMiddel_Middelen_MiddelenMiddelId",
                        column: x => x.MiddelenMiddelId,
                        principalTable: "Middelen",
                        principalColumn: "MiddelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LokaalMiddel_MiddelenMiddelId",
                table: "LokaalMiddel",
                column: "MiddelenMiddelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LokaalMiddel");

            migrationBuilder.AddColumn<string>(
                name: "LokaalId",
                table: "Middelen",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Middelen_LokaalId",
                table: "Middelen",
                column: "LokaalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Middelen_Lokalen_LokaalId",
                table: "Middelen",
                column: "LokaalId",
                principalTable: "Lokalen",
                principalColumn: "LokaalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
