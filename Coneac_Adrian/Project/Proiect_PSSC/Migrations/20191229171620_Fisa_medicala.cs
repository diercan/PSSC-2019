using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect_PSSC.Migrations
{
    public partial class Fisa_medicala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Boala",
                table: "Pacienti");

            migrationBuilder.AddColumn<float>(
                name: "Auscultatie",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Eritrocite",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Frecv_Respiratorie",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Frecventa_Cardiaca",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Hematocrit",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Hemoglobina",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "IMC",
                table: "Pacienti",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Inspectie_Toracica",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Leucocite",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Puls",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Temperatura",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Tensiune",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Trombocite",
                table: "Pacienti",
                nullable: false,
                defaultValue: 0f);
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
                name: "IMC",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Inspectie_Toracica",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Leucocite",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Puls",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Temperatura",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Tensiune",
                table: "Pacienti");

            migrationBuilder.DropColumn(
                name: "Trombocite",
                table: "Pacienti");

            migrationBuilder.AddColumn<string>(
                name: "Boala",
                table: "Pacienti",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pacienti",
                keyColumn: "Id",
                keyValue: 1,
                column: "Boala",
                value: "Raceala");
        }
    }
}
