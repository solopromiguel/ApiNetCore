using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication21.Migrations
{
    public partial class AddDescripcionControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Control",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Control");
        }
    }
}
