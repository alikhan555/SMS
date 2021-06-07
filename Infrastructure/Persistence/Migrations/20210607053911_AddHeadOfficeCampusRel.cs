using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddHeadOfficeCampusRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HeadOffice_CampusId",
                table: "HeadOffice",
                column: "CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadOffice_Campus_CampusId",
                table: "HeadOffice",
                column: "CampusId",
                principalTable: "Campus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadOffice_Campus_CampusId",
                table: "HeadOffice");

            migrationBuilder.DropIndex(
                name: "IX_HeadOffice_CampusId",
                table: "HeadOffice");
        }
    }
}
