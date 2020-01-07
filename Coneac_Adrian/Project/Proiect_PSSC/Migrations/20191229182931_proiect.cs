using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect_PSSC.Migrations
{
    public partial class proiect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Trombocite",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Tensiune",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Temperatura",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Puls",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Leucocite",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Inspectie_Toracica",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Hemoglobina",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Hematocrit",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Frecventa_Cardiaca",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Frecv_Respiratorie",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Eritrocite",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Auscultatie",
                table: "Pacienti",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "Pacienti",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Auscultatie", "Eritrocite", "Frecv_Respiratorie", "Frecventa_Cardiaca", "Hematocrit", "Hemoglobina", "Inspectie_Toracica", "Leucocite", "Puls", "Temperatura", "Tensiune", "Trombocite" },
                values: new object[] { null, null, null, null, null, null, null, null, null, null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Trombocite",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Tensiune",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Temperatura",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Puls",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Leucocite",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Inspectie_Toracica",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Hemoglobina",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Hematocrit",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Frecventa_Cardiaca",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Frecv_Respiratorie",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Eritrocite",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Auscultatie",
                table: "Pacienti",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Pacienti",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Auscultatie", "Eritrocite", "Frecv_Respiratorie", "Frecventa_Cardiaca", "Hematocrit", "Hemoglobina", "Inspectie_Toracica", "Leucocite", "Puls", "Temperatura", "Tensiune", "Trombocite" },
                values: new object[] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f });
        }
    }
}
