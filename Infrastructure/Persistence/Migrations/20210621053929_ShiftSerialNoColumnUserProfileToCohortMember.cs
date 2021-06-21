using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class ShiftSerialNoColumnUserProfileToCohortMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "UserProfile");

            migrationBuilder.AddColumn<int>(
                name: "SerialNo",
                table: "CohortMember",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "CohortMember");

            migrationBuilder.AddColumn<int>(
                name: "SerialNo",
                table: "UserProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
