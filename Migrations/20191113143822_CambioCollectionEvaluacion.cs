using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class CambioCollectionEvaluacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalisisProductos_Identificacions_IdentificacionId",
                table: "AnalisisProductos");

            migrationBuilder.DropIndex(
                name: "IX_AnalisisProductos_IdentificacionId",
                table: "AnalisisProductos");

            migrationBuilder.DropColumn(
                name: "IdentificacionId",
                table: "AnalisisProductos");

            migrationBuilder.AddColumn<int>(
                name: "AnalisisProductoId",
                table: "Identificacions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identificacions_AnalisisProductoId",
                table: "Identificacions",
                column: "AnalisisProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Identificacions_AnalisisProductos_AnalisisProductoId",
                table: "Identificacions",
                column: "AnalisisProductoId",
                principalTable: "AnalisisProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Identificacions_AnalisisProductos_AnalisisProductoId",
                table: "Identificacions");

            migrationBuilder.DropIndex(
                name: "IX_Identificacions_AnalisisProductoId",
                table: "Identificacions");

            migrationBuilder.DropColumn(
                name: "AnalisisProductoId",
                table: "Identificacions");

            migrationBuilder.AddColumn<int>(
                name: "IdentificacionId",
                table: "AnalisisProductos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AnalisisProductos_IdentificacionId",
                table: "AnalisisProductos",
                column: "IdentificacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalisisProductos_Identificacions_IdentificacionId",
                table: "AnalisisProductos",
                column: "IdentificacionId",
                principalTable: "Identificacions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
