using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanszowkaPlusPlus.Migrations
{
    /// <inheritdoc />
    public partial class fixed_date_only_reservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Employees",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReservationDate",
                table: "Reservations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Employees",
                newName: "Password");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReservationDate",
                table: "Reservations",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
