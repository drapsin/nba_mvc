using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_mvc.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGames4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameTime",
                table: "Game");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameTime",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
