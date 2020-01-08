using Microsoft.EntityFrameworkCore.Migrations;

namespace FrBaschet.Infrastructure.Migrations
{
    public partial class commisioner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommissionerId",
                table: "GameEntityModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referee1Id",
                table: "GameEntityModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referee2Id",
                table: "GameEntityModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameEntityModels_CommissionerId",
                table: "GameEntityModels",
                column: "CommissionerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameEntityModels_Referee1Id",
                table: "GameEntityModels",
                column: "Referee1Id");

            migrationBuilder.CreateIndex(
                name: "IX_GameEntityModels_Referee2Id",
                table: "GameEntityModels",
                column: "Referee2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameEntityModels_AspNetUsers_CommissionerId",
                table: "GameEntityModels",
                column: "CommissionerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameEntityModels_AspNetUsers_Referee1Id",
                table: "GameEntityModels",
                column: "Referee1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameEntityModels_AspNetUsers_Referee2Id",
                table: "GameEntityModels",
                column: "Referee2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameEntityModels_AspNetUsers_CommissionerId",
                table: "GameEntityModels");

            migrationBuilder.DropForeignKey(
                name: "FK_GameEntityModels_AspNetUsers_Referee1Id",
                table: "GameEntityModels");

            migrationBuilder.DropForeignKey(
                name: "FK_GameEntityModels_AspNetUsers_Referee2Id",
                table: "GameEntityModels");

            migrationBuilder.DropIndex(
                name: "IX_GameEntityModels_CommissionerId",
                table: "GameEntityModels");

            migrationBuilder.DropIndex(
                name: "IX_GameEntityModels_Referee1Id",
                table: "GameEntityModels");

            migrationBuilder.DropIndex(
                name: "IX_GameEntityModels_Referee2Id",
                table: "GameEntityModels");

            migrationBuilder.DropColumn(
                name: "CommissionerId",
                table: "GameEntityModels");

            migrationBuilder.DropColumn(
                name: "Referee1Id",
                table: "GameEntityModels");

            migrationBuilder.DropColumn(
                name: "Referee2Id",
                table: "GameEntityModels");
        }
    }
}
