using Microsoft.EntityFrameworkCore.Migrations;

namespace Leerplatform.Migrations
{
    public partial class studentenplanningen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanningUser",
                columns: table => new
                {
                    PlanningenPlanningId = table.Column<int>(type: "int", nullable: false),
                    StudentenId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningUser", x => new { x.PlanningenPlanningId, x.StudentenId });
                    table.ForeignKey(
                        name: "FK_PlanningUser_AspNetUsers_StudentenId",
                        column: x => x.StudentenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanningUser_Planningen_PlanningenPlanningId",
                        column: x => x.PlanningenPlanningId,
                        principalTable: "Planningen",
                        principalColumn: "PlanningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanningUser_StudentenId",
                table: "PlanningUser",
                column: "StudentenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanningUser");
        }
    }
}
