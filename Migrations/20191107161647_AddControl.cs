using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class AddControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Factors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Factors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Factors");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Factors");
        }
    }
}
