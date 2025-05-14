using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Team",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Team");
        }
    }
}
