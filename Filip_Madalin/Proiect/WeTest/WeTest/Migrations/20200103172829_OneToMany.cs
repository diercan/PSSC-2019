using Microsoft.EntityFrameworkCore.Migrations;

namespace WeTest.Migrations
{
    public partial class OneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Tester_AuthorTesterId",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_AuthorTesterId",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "AuthorTesterId",
                table: "Test");

            migrationBuilder.AlterColumn<string>(
                name: "TesterName",
                table: "Tester",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TestTitle",
                table: "Test",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TesterId",
                table: "Test",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_TesterId",
                table: "Test",
                column: "TesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Tester_TesterId",
                table: "Test",
                column: "TesterId",
                principalTable: "Tester",
                principalColumn: "TesterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Tester_TesterId",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_TesterId",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "TesterId",
                table: "Test");

            migrationBuilder.AlterColumn<string>(
                name: "TesterName",
                table: "Tester",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "TestTitle",
                table: "Test",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "AuthorTesterId",
                table: "Test",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_AuthorTesterId",
                table: "Test",
                column: "AuthorTesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Tester_AuthorTesterId",
                table: "Test",
                column: "AuthorTesterId",
                principalTable: "Tester",
                principalColumn: "TesterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
