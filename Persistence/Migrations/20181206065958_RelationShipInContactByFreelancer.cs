using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RelationShipInContactByFreelancer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_ApplicationUserId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "FromId",
                table: "Contacts",
                newName: "FreelancerId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreelancerId1",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FreelancerId1",
                table: "Contacts",
                column: "FreelancerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Freelancers_FreelancerId1",
                table: "Contacts",
                column: "FreelancerId1",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_ApplicationUserId",
                table: "Ratings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Freelancers_FreelancerId1",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_ApplicationUserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_FreelancerId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FreelancerId1",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "FreelancerId",
                table: "Contacts",
                newName: "FromId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Ratings",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_ApplicationUserId",
                table: "Ratings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
