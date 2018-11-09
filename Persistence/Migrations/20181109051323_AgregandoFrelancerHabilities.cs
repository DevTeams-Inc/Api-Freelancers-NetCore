using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AgregandoFrelancerHabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Freelancers_Habilities_HabilityId",
                table: "Freelancers");

            migrationBuilder.DropIndex(
                name: "IX_Freelancers_HabilityId",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "HabilityId",
                table: "Freelancers");

            migrationBuilder.CreateTable(
                name: "FreelancerHabilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FreelancerId = table.Column<int>(nullable: false),
                    HabilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerHabilities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreelancerHabilities");

            migrationBuilder.AddColumn<int>(
                name: "HabilityId",
                table: "Freelancers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Freelancers_HabilityId",
                table: "Freelancers",
                column: "HabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Freelancers_Habilities_HabilityId",
                table: "Freelancers",
                column: "HabilityId",
                principalTable: "Habilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
