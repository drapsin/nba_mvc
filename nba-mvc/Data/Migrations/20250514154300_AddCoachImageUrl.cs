using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCoachImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Coach",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Coach");
        }
    }
}
