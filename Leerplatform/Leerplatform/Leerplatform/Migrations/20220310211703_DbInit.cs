using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Leerplatform.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lokalen",
                columns: table => new
                {
                    LokaalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capaciteit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokalen", x => x.LokaalId);
                });

            migrationBuilder.CreateTable(
                name: "Planningen",
                columns: table => new
                {
                    PlanningId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planningen", x => x.PlanningId);
                });

            migrationBuilder.CreateTable(
                name: "Vakken",
                columns: table => new
                {
                    VakId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Studiepunten = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vakken", x => x.VakId);
                });

            migrationBuilder.CreateTable(
                name: "Middelen",
                columns: table => new
                {
                    MiddelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LokaalId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Middelen", x => x.MiddelId);
                    table.ForeignKey(
                        name: "FK_Middelen_Lokalen_LokaalId",
                        column: x => x.LokaalId,
                        principalTable: "Lokalen",
                        principalColumn: "LokaalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessen",
                columns: table => new
                {
                    LesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tijdstip = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VakId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LokaalId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlanningId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessen", x => x.LesId);
                    table.ForeignKey(
                        name: "FK_Lessen_Lokalen_LokaalId",
                        column: x => x.LokaalId,
                        principalTable: "Lokalen",
                        principalColumn: "LokaalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lessen_Planningen_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planningen",
                        principalColumn: "PlanningId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lessen_Vakken_VakId",
                        column: x => x.VakId,
                        principalTable: "Vakken",
                        principalColumn: "VakId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessen_LokaalId",
                table: "Lessen",
                column: "LokaalId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessen_PlanningId",
                table: "Lessen",
                column: "PlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessen_VakId",
                table: "Lessen",
                column: "VakId");

            migrationBuilder.CreateIndex(
                name: "IX_Middelen_LokaalId",
                table: "Middelen",
                column: "LokaalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lessen");

            migrationBuilder.DropTable(
                name: "Middelen");

            migrationBuilder.DropTable(
                name: "Planningen");

            migrationBuilder.DropTable(
                name: "Vakken");

            migrationBuilder.DropTable(
                name: "Lokalen");
        }
    }
}
