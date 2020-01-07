using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect_PSSC.Migrations
{
    public partial class Puls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IMC",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Auscultatie",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Eritrocite",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Frecv_Respiratorie",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Frecventa_Cardiaca",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hematocrit",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hemoglobina",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IMC",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inspectie_Toracica",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Leucocite",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tensiune",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trombocite",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
