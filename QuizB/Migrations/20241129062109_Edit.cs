using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizB.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Cards_CardId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Cards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardId",
                table: "Cards",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Cards_CardId",
                table: "Cards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");
        }
    }
}
