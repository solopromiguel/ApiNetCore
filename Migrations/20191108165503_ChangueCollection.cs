using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class ChangueCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caracteristicas_Caracteristicas_CaracteristicaId",
                table: "Caracteristicas");

            migrationBuilder.DropIndex(
                name: "IX_Caracteristicas_CaracteristicaId",
                table: "Caracteristicas");

            migrationBuilder.DropColumn(
                name: "CaracteristicaId",
                table: "Caracteristicas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaracteristicaId",
                table: "Caracteristicas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Caracteristicas_CaracteristicaId",
                table: "Caracteristicas",
                column: "CaracteristicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caracteristicas_Caracteristicas_CaracteristicaId",
                table: "Caracteristicas",
                column: "CaracteristicaId",
                principalTable: "Caracteristicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
