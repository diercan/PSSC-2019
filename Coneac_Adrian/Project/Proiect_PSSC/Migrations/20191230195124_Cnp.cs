using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect_PSSC.Migrations
{
    public partial class Cnp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CNP",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Pacienti",
                keyColumn: "Id",
                keyValue: 1,
                column: "CNP",
                value: "12431231412");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CNP",
                table: "Pacienti",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Pacienti",
                keyColumn: "Id",
                keyValue: 1,
                column: "CNP",
                value: 12431231412L);
        }
    }
}
