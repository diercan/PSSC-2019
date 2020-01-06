using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPlanner.Migrations
{
    public partial class Profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "other_ocupation",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Userid",
                table: "MyTask",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Userid1",
                table: "MyTask",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyTask_Userid",
                table: "MyTask",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_MyTask_Userid1",
                table: "MyTask",
                column: "Userid1");

            migrationBuilder.AddForeignKey(
                name: "FK_MyTask_User_Userid",
                table: "MyTask",
                column: "Userid",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyTask_User_Userid1",
                table: "MyTask",
                column: "Userid1",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyTask_User_Userid",
                table: "MyTask");

            migrationBuilder.DropForeignKey(
                name: "FK_MyTask_User_Userid1",
                table: "MyTask");

            migrationBuilder.DropIndex(
                name: "IX_MyTask_Userid",
                table: "MyTask");

            migrationBuilder.DropIndex(
                name: "IX_MyTask_Userid1",
                table: "MyTask");

            migrationBuilder.DropColumn(
                name: "age",
                table: "User");

            migrationBuilder.DropColumn(
                name: "other_ocupation",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "MyTask");

            migrationBuilder.DropColumn(
                name: "Userid1",
                table: "MyTask");
        }
    }
}
