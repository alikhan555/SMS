using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddCohortMemberRelWithCohort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CohortMember_CohortId",
                table: "CohortMember",
                column: "CohortId");

            migrationBuilder.AddForeignKey(
                name: "FK_CohortMember_Cohort_CohortId",
                table: "CohortMember",
                column: "CohortId",
                principalTable: "Cohort",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CohortMember_Cohort_CohortId",
                table: "CohortMember");

            migrationBuilder.DropIndex(
                name: "IX_CohortMember_CohortId",
                table: "CohortMember");
        }
    }
}
