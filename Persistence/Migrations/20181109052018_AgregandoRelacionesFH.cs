using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AgregandoRelacionesFH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FreelancerHabilities_FreelancerId",
                table: "FreelancerHabilities",
                column: "FreelancerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerHabilities_HabilityId",
                table: "FreelancerHabilities",
                column: "HabilityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerHabilities_Freelancers_FreelancerId",
                table: "FreelancerHabilities",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelancerHabilities_Habilities_HabilityId",
                table: "FreelancerHabilities",
                column: "HabilityId",
                principalTable: "Habilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerHabilities_Freelancers_FreelancerId",
                table: "FreelancerHabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_FreelancerHabilities_Habilities_HabilityId",
                table: "FreelancerHabilities");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerHabilities_FreelancerId",
                table: "FreelancerHabilities");

            migrationBuilder.DropIndex(
                name: "IX_FreelancerHabilities_HabilityId",
                table: "FreelancerHabilities");
        }
    }
}
