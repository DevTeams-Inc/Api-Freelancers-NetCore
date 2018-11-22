using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AnswersFreelancer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Answers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ApplicationUserId",
                table: "Answers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_ApplicationUserId",
                table: "Answers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_ApplicationUserId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ApplicationUserId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Answers");
        }
    }
}
