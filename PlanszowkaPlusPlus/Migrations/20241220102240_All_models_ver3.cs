using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanszowkaPlusPlus.Migrations
{
    /// <inheritdoc />
    public partial class All_models_ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Games_GameId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Members_MemberId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_GameTables_TableId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Members_MemberId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_MemberId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_GameId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MemberId",
                table: "Rentals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reservations_MemberId",
                table: "Reservations",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_GameId",
                table: "Rentals",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MemberId",
                table: "Rentals",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Games_GameId",
                table: "Rentals",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Members_MemberId",
                table: "Rentals",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_GameTables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "GameTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Members_MemberId",
                table: "Reservations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
