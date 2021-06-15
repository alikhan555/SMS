using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddUserProfileRelWithAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_AspNetUsers_AppUserId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_AppUserId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_AspNetUsers_Id",
                table: "UserProfile",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_AspNetUsers_Id",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "UserProfile",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_AppUserId",
                table: "UserProfile",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_AspNetUsers_AppUserId",
                table: "UserProfile",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
