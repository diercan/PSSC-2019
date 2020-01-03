using Microsoft.EntityFrameworkCore.Migrations;

namespace WeTest.Migrations
{
    public partial class OptionalRelationTesterId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Tester_TesterId",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_TesterId",
                table: "Test");

            migrationBuilder.AlterColumn<int>(
                name: "TesterId",
                table: "Test",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TesterId1",
                table: "Test",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Test_TesterId1",
                table: "Test",
                column: "TesterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Tester_TesterId1",
                table: "Test",
                column: "TesterId1",
                principalTable: "Tester",
                principalColumn: "TesterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Tester_TesterId1",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_TesterId1",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "TesterId1",
                table: "Test");

            migrationBuilder.AlterColumn<string>(
                name: "TesterId",
                table: "Test",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

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
    }
}
