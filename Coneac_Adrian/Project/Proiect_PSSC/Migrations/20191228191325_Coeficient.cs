using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect_PSSC.Migrations
{
    public partial class Coeficient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Greutate",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inaltime",
                table: "Pacienti",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Greutate",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Inaltime",
                table: "Pacienti");
        }
    }
}
