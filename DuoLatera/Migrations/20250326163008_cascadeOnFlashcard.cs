using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuoLatera.Migrations
{
    /// <inheritdoc />
    public partial class cascadeOnFlashcard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCards_CardSets_CardSetId",
                table: "FlashCards");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCards_CardSets_CardSetId",
                table: "FlashCards",
                column: "CardSetId",
                principalTable: "CardSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashCards_CardSets_CardSetId",
                table: "FlashCards");

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCards_CardSets_CardSetId",
                table: "FlashCards",
                column: "CardSetId",
                principalTable: "CardSets",
                principalColumn: "Id");
        }
    }
}
