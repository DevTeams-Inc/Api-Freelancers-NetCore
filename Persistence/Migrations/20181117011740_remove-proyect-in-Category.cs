using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class removeproyectinCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Proyects_CategoryId",
                table: "Proyects");

            migrationBuilder.CreateIndex(
                name: "IX_Proyects_CategoryId",
                table: "Proyects",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Proyects_CategoryId",
                table: "Proyects");

            migrationBuilder.CreateIndex(
                name: "IX_Proyects_CategoryId",
                table: "Proyects",
                column: "CategoryId",
                unique: true);
        }
    }
}
