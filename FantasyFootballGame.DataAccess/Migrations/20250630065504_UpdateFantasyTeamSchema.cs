using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyFootballGame.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFantasyTeamSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeTransfers",
                table: "GameweekTeams");

            migrationBuilder.DropColumn(
                name: "TotalTransfers",
                table: "GameweekTeams");

            migrationBuilder.RenameColumn(
                name: "UsedTransfers",
                table: "GameweekTeams",
                newName: "TransfersMade");

            migrationBuilder.AddColumn<int>(
                name: "FreeTransfers",
                table: "FantasyTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeTransfers",
                table: "FantasyTeams");

            migrationBuilder.RenameColumn(
                name: "TransfersMade",
                table: "GameweekTeams",
                newName: "UsedTransfers");

            migrationBuilder.AddColumn<int>(
                name: "FreeTransfers",
                table: "GameweekTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTransfers",
                table: "GameweekTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
