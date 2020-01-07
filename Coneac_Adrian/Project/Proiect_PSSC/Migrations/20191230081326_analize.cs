using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect_PSSC.Migrations
{
    public partial class analize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Auscultatie",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Eritrocite",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Frecv_Respiratorie",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Frecventa_Cardiaca",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hematocrit",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hemoglobina",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inspectie_Toracica",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Leucocite",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tensiune",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trombocite",
                table: "Pacienti",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auscultatie",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Eritrocite",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Frecv_Respiratorie",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Frecventa_Cardiaca",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Hematocrit",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Hemoglobina",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Inspectie_Toracica",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Leucocite",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Tensiune",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Trombocite",
                table: "Pacienti");
        }
    }
}
