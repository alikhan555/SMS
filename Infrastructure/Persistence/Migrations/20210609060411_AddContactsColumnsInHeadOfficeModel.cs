using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddContactsColumnsInHeadOfficeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contact1",
                table: "HeadOffice",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact2",
                table: "HeadOffice",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact3",
                table: "HeadOffice",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact1",
                table: "HeadOffice");

            migrationBuilder.DropColumn(
                name: "Contact2",
                table: "HeadOffice");

            migrationBuilder.DropColumn(
                name: "Contact3",
                table: "HeadOffice");
        }
    }
}
