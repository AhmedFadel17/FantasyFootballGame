using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyFootballGame.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlayerStatsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Points",
                table: "PlayerStats",
                newName: "TotalPoints");

            migrationBuilder.RenameColumn(
                name: "GameweekId",
                table: "PlayerStats",
                newName: "SeasonNumber");

            migrationBuilder.RenameColumn(
                name: "FixtureId",
                table: "PlayerStats",
                newName: "MatchesStarted");

            migrationBuilder.AddColumn<int>(
                name: "MatchesPlayed",
                table: "PlayerStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlayerGameweekStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    GameweekId = table.Column<int>(type: "int", nullable: false),
                    FixtureId = table.Column<int>(type: "int", nullable: false),
                    IsStarting = table.Column<bool>(type: "bit", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false),
                    GoalsScored = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    OwnGoals = table.Column<int>(type: "int", nullable: false),
                    CleanSheets = table.Column<int>(type: "int", nullable: false),
                    Saves = table.Column<int>(type: "int", nullable: false),
                    PenaltyMisses = table.Column<int>(type: "int", nullable: false),
                    PenaltySaved = table.Column<int>(type: "int", nullable: false),
                    YellowCards = table.Column<int>(type: "int", nullable: false),
                    RedCards = table.Column<int>(type: "int", nullable: false),
                    BonusPoints = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerGameweekStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerGameweekStats_Fixtures_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixtures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerGameweekStats_Gameweeks_GameweekId",
                        column: x => x.GameweekId,
                        principalTable: "Gameweeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerGameweekStats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_PlayerId",
                table: "PlayerStats",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGameweekStats_FixtureId",
                table: "PlayerGameweekStats",
                column: "FixtureId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGameweekStats_GameweekId",
                table: "PlayerGameweekStats",
                column: "GameweekId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerGameweekStats_PlayerId",
                table: "PlayerGameweekStats",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStats_Players_PlayerId",
                table: "PlayerStats",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStats_Players_PlayerId",
                table: "PlayerStats");

            migrationBuilder.DropTable(
                name: "PlayerGameweekStats");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStats_PlayerId",
                table: "PlayerStats");

            migrationBuilder.DropColumn(
                name: "MatchesPlayed",
                table: "PlayerStats");

            migrationBuilder.RenameColumn(
                name: "TotalPoints",
                table: "PlayerStats",
                newName: "Points");

            migrationBuilder.RenameColumn(
                name: "SeasonNumber",
                table: "PlayerStats",
                newName: "GameweekId");

            migrationBuilder.RenameColumn(
                name: "MatchesStarted",
                table: "PlayerStats",
                newName: "FixtureId");
        }
    }
}
