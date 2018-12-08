using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RelationShipInContactByFreelancer2x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Freelancers_FreelancerId1",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_FreelancerId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FreelancerId1",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "FreelancerId",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_FreelancerId",
                table: "Contacts",
                column: "FreelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Freelancers_FreelancerId",
                table: "Contacts",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Freelancers_FreelancerId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_FreelancerId",
                table: "Contacts");

            migrationBuilder.AlterColumn<string>(
                name: "FreelancerId",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(int));

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
        }
    }
}
