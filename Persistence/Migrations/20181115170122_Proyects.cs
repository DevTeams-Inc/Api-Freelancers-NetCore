using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Proyects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Proyects_ProyectId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Proyects_ProyectId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_ProyectId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProyectId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProyectId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Proyects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProyectId",
                table: "Proposals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proyects_CategoryId",
                table: "Proyects",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProyectId",
                table: "Proposals",
                column: "ProyectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Proyects_ProyectId",
                table: "Proposals",
                column: "ProyectId",
                principalTable: "Proyects",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Proyects_Categories_CategoryId",
                table: "Proyects",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Proyects_ProyectId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyects_Categories_CategoryId",
                table: "Proyects");

            migrationBuilder.DropIndex(
                name: "IX_Proyects_CategoryId",
                table: "Proyects");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_ProyectId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Proyects");

            migrationBuilder.AlterColumn<int>(
                name: "ProyectId",
                table: "Proposals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProyectId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProyectId",
                table: "Proposals",
                column: "ProyectId",
                unique: true,
                filter: "[ProyectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProyectId",
                table: "Categories",
                column: "ProyectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Proyects_ProyectId",
                table: "Categories",
                column: "ProyectId",
                principalTable: "Proyects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Proyects_ProyectId",
                table: "Proposals",
                column: "ProyectId",
                principalTable: "Proyects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
