using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect_PSSC.Migrations
{
    public partial class SeedPacientiTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pacienti",
                columns: new[] { "Id", "Adresa", "CNP", "Nume", "Prenume", "Sexul" },
                values: new object[] { 1, "Sacalaz", 12431231412L, "Adi", "Ion", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pacienti",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
