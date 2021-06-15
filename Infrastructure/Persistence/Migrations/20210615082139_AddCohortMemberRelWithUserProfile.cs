using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddCohortMemberRelWithUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CohortId",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "MemberId1",
                table: "CohortMember",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CohortMember_MemberId1",
                table: "CohortMember",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CohortMember_UserProfile_MemberId1",
                table: "CohortMember",
                column: "MemberId1",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CohortMember_UserProfile_MemberId1",
                table: "CohortMember");

            migrationBuilder.DropIndex(
                name: "IX_CohortMember_MemberId1",
                table: "CohortMember");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "CohortMember");

            migrationBuilder.AddColumn<int>(
                name: "CohortId",
                table: "UserProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
