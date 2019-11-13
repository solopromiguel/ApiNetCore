using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class AddIdentificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentificacionId",
                table: "AnalisisProductos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Identificacions",
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
                    table.PrimaryKey("PK_Identificacions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identificacions_AnalisisProductos_AnalisisProductoId",
                        column: x => x.AnalisisProductoId,
                        principalTable: "AnalisisProductos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identificacions_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalisisProductos_IdentificacionId",
                table: "AnalisisProductos",
                column: "IdentificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Identificacions_AnalisisProductoId",
                table: "Identificacions",
                column: "AnalisisProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Identificacions_UsersId",
                table: "Identificacions",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalisisProductos_Identificacions_IdentificacionId",
                table: "AnalisisProductos",
                column: "IdentificacionId",
                principalTable: "Identificacions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalisisProductos_Identificacions_IdentificacionId",
                table: "AnalisisProductos");

            migrationBuilder.DropTable(
                name: "Identificacions");

            migrationBuilder.DropIndex(
                name: "IX_AnalisisProductos_IdentificacionId",
                table: "AnalisisProductos");

            migrationBuilder.DropColumn(
                name: "IdentificacionId",
                table: "AnalisisProductos");
        }
    }
}
