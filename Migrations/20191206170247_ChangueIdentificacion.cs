using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class ChangueIdentificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Riesgos_Caracteristicas_CaracteristicaId",
                table: "Riesgos");

            migrationBuilder.RenameColumn(
                name: "CaracteristicaId",
                table: "Riesgos",
                newName: "IdentificacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Riesgos_CaracteristicaId",
                table: "Riesgos",
                newName: "IX_Riesgos_IdentificacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Riesgos_Identificacions_IdentificacionId",
                table: "Riesgos",
                column: "IdentificacionId",
                principalTable: "Identificacions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Riesgos_Identificacions_IdentificacionId",
                table: "Riesgos");

            migrationBuilder.RenameColumn(
                name: "IdentificacionId",
                table: "Riesgos",
                newName: "CaracteristicaId");

            migrationBuilder.RenameIndex(
                name: "IX_Riesgos_IdentificacionId",
                table: "Riesgos",
                newName: "IX_Riesgos_CaracteristicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Riesgos_Caracteristicas_CaracteristicaId",
                table: "Riesgos",
                column: "CaracteristicaId",
                principalTable: "Caracteristicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
