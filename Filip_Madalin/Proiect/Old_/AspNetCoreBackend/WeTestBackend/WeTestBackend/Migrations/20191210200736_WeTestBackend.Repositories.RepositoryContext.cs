using Microsoft.EntityFrameworkCore.Migrations;

namespace WeTestBackend.Migrations
{
    public partial class WeTestBackendRepositoriesRepositoryContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Testers",
                columns: table => new
                {
                    TesterId = table.Column<string>(nullable: false),
                    TesterName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testers", x => x.TesterId);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    TestId = table.Column<string>(nullable: false),
                    TestTitle = table.Column<string>(nullable: true),
                    AuthorTesterId = table.Column<string>(nullable: true),
                    Functionality = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_Tests_Testers_AuthorTesterId",
                        column: x => x.AuthorTesterId,
                        principalTable: "Testers",
                        principalColumn: "TesterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_AuthorTesterId",
                table: "Tests",
                column: "AuthorTesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "Testers");
        }
    }
}
