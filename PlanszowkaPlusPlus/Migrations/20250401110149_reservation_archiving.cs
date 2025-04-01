using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanszowkaPlusPlus.Migrations
{
    /// <inheritdoc />
    public partial class reservation_archiving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Reservations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Reservations");
        }
    }
}
