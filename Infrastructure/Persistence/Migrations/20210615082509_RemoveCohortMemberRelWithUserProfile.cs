using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class RemoveCohortMemberRelWithUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
