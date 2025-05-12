using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nba_mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReferee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Referee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Licence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referee", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Referee");
        }
    }
}
