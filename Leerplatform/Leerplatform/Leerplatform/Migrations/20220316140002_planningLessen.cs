using Microsoft.EntityFrameworkCore.Migrations;

namespace Leerplatform.Migrations
{
    public partial class planningLessen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessen_Planningen_PlanningId",
                table: "Lessen");

            migrationBuilder.AlterColumn<int>(
                name: "PlanningId",
                table: "Lessen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessen_Planningen_PlanningId",
                table: "Lessen",
                column: "PlanningId",
                principalTable: "Planningen",
                principalColumn: "PlanningId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessen_Planningen_PlanningId",
                table: "Lessen");

            migrationBuilder.AlterColumn<int>(
                name: "PlanningId",
                table: "Lessen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessen_Planningen_PlanningId",
                table: "Lessen",
                column: "PlanningId",
                principalTable: "Planningen",
                principalColumn: "PlanningId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
