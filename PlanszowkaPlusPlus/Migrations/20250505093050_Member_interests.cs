using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanszowkaPlusPlus.Migrations
{
    /// <inheritdoc />
    public partial class Member_interests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "Members",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interests",
                table: "Members");
        }
    }
}
