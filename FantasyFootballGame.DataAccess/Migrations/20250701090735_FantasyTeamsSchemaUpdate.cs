using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyFootballGame.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FantasyTeamsSchemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasUnlimitedTransfers",
                table: "GameweekTeams");

            migrationBuilder.AddColumn<int>(
                name: "Chip",
                table: "GameweekTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasUnlimitedTransfers",
                table: "FantasyTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chip",
                table: "GameweekTeams");

            migrationBuilder.DropColumn(
                name: "HasUnlimitedTransfers",
                table: "FantasyTeams");

            migrationBuilder.AddColumn<bool>(
                name: "HasUnlimitedTransfers",
                table: "GameweekTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
