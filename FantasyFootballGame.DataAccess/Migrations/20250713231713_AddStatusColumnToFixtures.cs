using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyFootballGame.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusColumnToFixtures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fixtures");
        }
    }
}
