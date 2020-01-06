using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class SeedPhoneTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "Color", "Dimension", "Email", "Imagine", "Name", "PhotoPath", "Type" },
                values: new object[] { 1, "Silver", "10x20x30", null, null, "Iphone X", null, "XS MAX" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
