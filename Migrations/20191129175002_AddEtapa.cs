using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class AddEtapa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EtapaIdentificacionId",
                table: "Riesgos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Etapas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Area = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaCreate = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    UsersId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etapas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etapas_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Riesgos_EtapaIdentificacionId",
                table: "Riesgos",
                column: "EtapaIdentificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Etapas_UsersId",
                table: "Etapas",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Riesgos_Etapas_EtapaIdentificacionId",
                table: "Riesgos",
                column: "EtapaIdentificacionId",
                principalTable: "Etapas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Riesgos_Etapas_EtapaIdentificacionId",
                table: "Riesgos");

            migrationBuilder.DropTable(
                name: "Etapas");

            migrationBuilder.DropIndex(
                name: "IX_Riesgos_EtapaIdentificacionId",
                table: "Riesgos");

            migrationBuilder.DropColumn(
                name: "EtapaIdentificacionId",
                table: "Riesgos");
        }
    }
}
