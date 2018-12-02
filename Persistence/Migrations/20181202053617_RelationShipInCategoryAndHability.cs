using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RelationShipInCategoryAndHability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Habilities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Habilities_CategoryId",
                table: "Habilities",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habilities_Categories_CategoryId",
                table: "Habilities",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habilities_Categories_CategoryId",
                table: "Habilities");

            migrationBuilder.DropIndex(
                name: "IX_Habilities_CategoryId",
                table: "Habilities");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Habilities");
        }
    }
}
