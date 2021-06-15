using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddCohortMemberRelWithUserProfile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "CohortMember",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CohortMember_MemberId",
                table: "CohortMember",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_CohortMember_UserProfile_MemberId",
                table: "CohortMember",
                column: "MemberId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CohortMember_UserProfile_MemberId",
                table: "CohortMember");

            migrationBuilder.DropIndex(
                name: "IX_CohortMember_MemberId",
                table: "CohortMember");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "CohortMember",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
