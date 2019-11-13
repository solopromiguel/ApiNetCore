using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class Actualizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluacions_AnalisisProductos_AnalisisProductoId",
                table: "Evaluacions");

            migrationBuilder.DropForeignKey(
                name: "FK_Identificacions_AnalisisProductos_AnalisisProductoId",
                table: "Identificacions");

            migrationBuilder.DropTable(
                name: "IdentificacionProductos");

            migrationBuilder.DropTable(
                name: "AnalisisProductos");

            migrationBuilder.DropIndex(
                name: "IX_Identificacions_AnalisisProductoId",
                table: "Identificacions");

            migrationBuilder.DropIndex(
                name: "IX_Evaluacions_AnalisisProductoId",
                table: "Evaluacions");

            migrationBuilder.DropColumn(
                name: "AnalisisProductoId",
                table: "Identificacions");

            migrationBuilder.DropColumn(
                name: "AnalisisProductoId",
                table: "Evaluacions");

            migrationBuilder.AddColumn<int>(
                name: "IdentificacionBaseId",
                table: "Identificacions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdentificacionMainId",
                table: "Identificacions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Identificacions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ControlBaseId",
                table: "Control",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ControlMainId",
                table: "Control",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Control",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ControlRiesgos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Calificacion = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Formalizacion = table.Column<string>(nullable: true),
                    Grado = table.Column<string>(nullable: true),
                    Oportunidad = table.Column<string>(nullable: true),
                    Periodicidad = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    ControlId = table.Column<int>(nullable: false),
                    UsersId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlRiesgos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlRiesgos_Control_ControlId",
                        column: x => x.ControlId,
                        principalTable: "Control",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlRiesgos_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Riesgos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Calificacion = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Impacto = table.Column<string>(nullable: true),
                    Probabilidad = table.Column<string>(nullable: true),
                    RiesgoInherente = table.Column<string>(nullable: true),
                    RiesgoResidual = table.Column<string>(nullable: true),
                    UsersId = table.Column<int>(nullable: false),
                    CaracteristicaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riesgos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Riesgos_Caracteristicas_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "Caracteristicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Riesgos_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlRiesgos_ControlId",
                table: "ControlRiesgos",
                column: "ControlId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlRiesgos_UsersId",
                table: "ControlRiesgos",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Riesgos_CaracteristicaId",
                table: "Riesgos",
                column: "CaracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Riesgos_UsersId",
                table: "Riesgos",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlRiesgos");

            migrationBuilder.DropTable(
                name: "Riesgos");

            migrationBuilder.DropColumn(
                name: "IdentificacionBaseId",
                table: "Identificacions");

            migrationBuilder.DropColumn(
                name: "IdentificacionMainId",
                table: "Identificacions");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Identificacions");

            migrationBuilder.DropColumn(
                name: "ControlBaseId",
                table: "Control");

            migrationBuilder.DropColumn(
                name: "ControlMainId",
                table: "Control");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Control");

            migrationBuilder.AddColumn<int>(
                name: "AnalisisProductoId",
                table: "Identificacions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnalisisProductoId",
                table: "Evaluacions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnalisisProductos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Area = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    EvaluacionId = table.Column<int>(nullable: false),
                    FechaCreate = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "IdentificacionProductos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AnalisisProductoId = table.Column<int>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaCreate = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
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
                name: "IX_Identificacions_AnalisisProductoId",
                table: "Identificacions",
                column: "AnalisisProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluacions_AnalisisProductoId",
                table: "Evaluacions",
                column: "AnalisisProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisisProductos_EvaluacionId",
                table: "AnalisisProductos",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisisProductos_UsersId",
                table: "AnalisisProductos",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificacionProductos_AnalisisProductoId",
                table: "IdentificacionProductos",
                column: "AnalisisProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificacionProductos_UsersId",
                table: "IdentificacionProductos",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluacions_AnalisisProductos_AnalisisProductoId",
                table: "Evaluacions",
                column: "AnalisisProductoId",
                principalTable: "AnalisisProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Identificacions_AnalisisProductos_AnalisisProductoId",
                table: "Identificacions",
                column: "AnalisisProductoId",
                principalTable: "AnalisisProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
