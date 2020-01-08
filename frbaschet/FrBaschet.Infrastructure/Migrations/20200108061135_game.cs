using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FrBaschet.Infrastructure.Migrations
{
    public partial class game : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameEntityModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HomeTeamEntityModelId = table.Column<Guid>(nullable: true),
                    AwayTeamEntityModelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameEntityModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameEntityModels_Teams_AwayTeamEntityModelId",
                        column: x => x.AwayTeamEntityModelId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameEntityModels_Teams_HomeTeamEntityModelId",
                        column: x => x.HomeTeamEntityModelId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameEntityModels_AwayTeamEntityModelId",
                table: "GameEntityModels",
                column: "AwayTeamEntityModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GameEntityModels_HomeTeamEntityModelId",
                table: "GameEntityModels",
                column: "HomeTeamEntityModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameEntityModels");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
