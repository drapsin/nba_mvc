using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCoaches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coach_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coach_TeamId",
                table: "Coach",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coach");
        }
    }
}
