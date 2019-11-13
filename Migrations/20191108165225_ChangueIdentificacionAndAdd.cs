using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class ChangueIdentificacionAndAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Identificacions_AnalisisProductos_AnalisisProductoId",
                table: "Identificacions");

            migrationBuilder.DropColumn(
                name: "FechaCreate",
                table: "Identificacions");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Identificacions",
                newName: "Probabilidad");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "Identificacions",
                newName: "Impacto");

            migrationBuilder.RenameColumn(
                name: "AnalisisProductoId",
                table: "Identificacions",
                newName: "CaracteristicaId");

            migrationBuilder.RenameIndex(
                name: "IX_Identificacions_AnalisisProductoId",
                table: "Identificacions",
                newName: "IX_Identificacions_CaracteristicaId");

            migrationBuilder.AddColumn<string>(
                name: "Calificacion",
                table: "Identificacions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CaracteristicaId",
                table: "Caracteristicas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentificacionProductos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Area = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaCreate = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    AnalisisProductoId = table.Column<int>(nullable: false),
                    UsersId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificacionProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentificacionProductos_AnalisisProductos_AnalisisProductoId",
                        column: x => x.AnalisisProductoId,
                        principalTable: "AnalisisProductos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentificacionProductos_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caracteristicas_CaracteristicaId",
                table: "Caracteristicas",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificacionProductos_AnalisisProductoId",
                table: "IdentificacionProductos",
                column: "AnalisisProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificacionProductos_UsersId",
                table: "IdentificacionProductos",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caracteristicas_Caracteristicas_CaracteristicaId",
                table: "Caracteristicas",
                column: "CaracteristicaId",
                principalTable: "Caracteristicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Identificacions_Caracteristicas_CaracteristicaId",
                table: "Identificacions",
                column: "CaracteristicaId",
                principalTable: "Caracteristicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caracteristicas_Caracteristicas_CaracteristicaId",
                table: "Caracteristicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Identificacions_Caracteristicas_CaracteristicaId",
                table: "Identificacions");

            migrationBuilder.DropTable(
                name: "IdentificacionProductos");

            migrationBuilder.DropIndex(
                name: "IX_Caracteristicas_CaracteristicaId",
                table: "Caracteristicas");

            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "Identificacions");

            migrationBuilder.DropColumn(
                name: "CaracteristicaId",
                table: "Caracteristicas");

            migrationBuilder.RenameColumn(
                name: "Probabilidad",
                table: "Identificacions",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Impacto",
                table: "Identificacions",
                newName: "Area");

            migrationBuilder.RenameColumn(
                name: "CaracteristicaId",
                table: "Identificacions",
                newName: "AnalisisProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Identificacions_CaracteristicaId",
                table: "Identificacions",
                newName: "IX_Identificacions_AnalisisProductoId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreate",
                table: "Identificacions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Identificacions_AnalisisProductos_AnalisisProductoId",
                table: "Identificacions",
                column: "AnalisisProductoId",
                principalTable: "AnalisisProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
