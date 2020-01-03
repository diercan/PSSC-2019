using Microsoft.EntityFrameworkCore.Migrations;

namespace WeTest.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tester",
                columns: table => new
                {
                    TesterId = table.Column<string>(nullable: false),
                    TesterName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tester", x => x.TesterId);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    TestId = table.Column<string>(nullable: false),
                    TestTitle = table.Column<string>(nullable: true),
                    AuthorTesterId = table.Column<string>(nullable: true),
                    Functionality = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_Test_Tester_AuthorTesterId",
                        column: x => x.AuthorTesterId,
                        principalTable: "Tester",
                        principalColumn: "TesterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Test_AuthorTesterId",
                table: "Test",
                column: "AuthorTesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Tester");
        }
    }
}
