using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class AddAnalisisProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Controls_AspNetUsers_UsersId",
                table: "Controls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Controls",
                table: "Controls");

            migrationBuilder.RenameTable(
                name: "Controls",
                newName: "Control");

            migrationBuilder.RenameIndex(
                name: "IX_Controls_UsersId",
                table: "Control",
                newName: "IX_Control_UsersId");

            migrationBuilder.AddColumn<int>(
                name: "EvaluacionId",
                table: "Control",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Control",
                table: "Control",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Evaluacions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaCreate = table.Column<DateTime>(nullable: false),
                    Impacto = table.Column<string>(nullable: true),
                    Probabilidad = table.Column<string>(nullable: true),
                    Puntaje = table.Column<int>(nullable: false),
                    RiesgoInherente = table.Column<string>(nullable: true),
                    RiesgoResiual = table.Column<string>(nullable: true),
                    UsersId = table.Column<int>(nullable: false),
                    AnalisisProductoId = table.Column<int>(nullable: false),
                    FactorId = table.Column<int>(nullable: false),
                    CaracteristicaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluacions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluacions_Caracteristicas_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluacions_Factors_FactorId",
                        column: x => x.FactorId,
                        principalTable: "Factors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluacions_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalisisProductos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Area = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaCreate = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    EvaluacionId = table.Column<int>(nullable: false),
                    UsersId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisisProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalisisProductos_Evaluacions_EvaluacionId",
                        column: x => x.EvaluacionId,
                        principalTable: "Evaluacions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalisisProductos_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Control_EvaluacionId",
                table: "Control",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisisProductos_EvaluacionId",
                table: "AnalisisProductos",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisisProductos_UsersId",
                table: "AnalisisProductos",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluacions_AnalisisProductoId",
                table: "Evaluacions",
                column: "AnalisisProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluacions_CaracteristicaId",
                table: "Evaluacions",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluacions_FactorId",
                table: "Evaluacions",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluacions_UsersId",
                table: "Evaluacions",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Control_Evaluacions_EvaluacionId",
                table: "Control",
                column: "EvaluacionId",
                principalTable: "Evaluacions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Control_AspNetUsers_UsersId",
                table: "Control",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluacions_AnalisisProductos_AnalisisProductoId",
                table: "Evaluacions",
                column: "AnalisisProductoId",
                principalTable: "AnalisisProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Control_Evaluacions_EvaluacionId",
                table: "Control");

            migrationBuilder.DropForeignKey(
                name: "FK_Control_AspNetUsers_UsersId",
                table: "Control");

            migrationBuilder.DropForeignKey(
                name: "FK_AnalisisProductos_Evaluacions_EvaluacionId",
                table: "AnalisisProductos");

            migrationBuilder.DropTable(
                name: "Evaluacions");

            migrationBuilder.DropTable(
                name: "AnalisisProductos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Control",
                table: "Control");

            migrationBuilder.DropIndex(
                name: "IX_Control_EvaluacionId",
                table: "Control");

            migrationBuilder.DropColumn(
                name: "EvaluacionId",
                table: "Control");

            migrationBuilder.RenameTable(
                name: "Control",
                newName: "Controls");

            migrationBuilder.RenameIndex(
                name: "IX_Control_UsersId",
                table: "Controls",
                newName: "IX_Controls_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Controls",
                table: "Controls",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Controls_AspNetUsers_UsersId",
                table: "Controls",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
