using Microsoft.EntityFrameworkCore.Migrations;

namespace ProiectPSSC.Migrations
{
    public partial class TaxaAchitata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaxaAchitata",
                table: "Student",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxaAchitata",
                table: "Student");
        }
    }
}
