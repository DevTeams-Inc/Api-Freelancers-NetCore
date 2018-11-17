using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class DeleteProyectHabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProyectHabilities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProyectHabilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HabilityId = table.Column<int>(nullable: false),
                    ProyectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProyectHabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProyectHabilities_Habilities_HabilityId",
                        column: x => x.HabilityId,
                        principalTable: "Habilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProyectHabilities_Proyects_ProyectId",
                        column: x => x.ProyectId,
                        principalTable: "Proyects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProyectHabilities_HabilityId",
                table: "ProyectHabilities",
                column: "HabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProyectHabilities_ProyectId",
                table: "ProyectHabilities",
                column: "ProyectId");
        }
    }
}
