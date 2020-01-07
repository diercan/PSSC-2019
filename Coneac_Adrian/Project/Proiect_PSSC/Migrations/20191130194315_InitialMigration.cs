using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect_PSSC.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacienti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(nullable: true),
                    Prenume = table.Column<string>(nullable: true),
                    CNP = table.Column<long>(nullable: false),
                    Sexul = table.Column<int>(nullable: false),
                    Adresa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacienti", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacienti");
        }
    }
}
