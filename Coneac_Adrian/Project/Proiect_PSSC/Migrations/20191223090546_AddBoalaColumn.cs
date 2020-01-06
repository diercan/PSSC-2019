using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect_PSSC.Migrations
{
    public partial class AddBoalaColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Boala",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pacienti",
                keyColumn: "Id",
                keyValue: 1,
                column: "Boala",
                value: "Raceala");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Boala",
                table: "Pacienti");
        }
    }
}
