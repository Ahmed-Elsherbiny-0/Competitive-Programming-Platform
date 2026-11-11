using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addIndexOnVoteAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VoteAnswers_VoteId",
                table: "VoteAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_VoteAnswers_VoteId_QuestionId",
                table: "VoteAnswers",
                columns: new[] { "VoteId", "QuestionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VoteAnswers_VoteId_QuestionId",
                table: "VoteAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_VoteAnswers_VoteId",
                table: "VoteAnswers",
                column: "VoteId");
        }
    }
}
