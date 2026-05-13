using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmLogAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHashed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "WatchedLists",
                columns: table => new
                {
                    WatchedListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Actor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesWatched = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedLists", x => x.WatchedListId);
                    table.ForeignKey(
                        name: "FK_WatchedLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchLists",
                columns: table => new
                {
                    WatchListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Actor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLists", x => x.WatchListId);
                    table.ForeignKey(
                        name: "FK_WatchLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchedLists_UserId",
                table: "WatchedLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLists_UserId",
                table: "WatchLists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchedLists");

            migrationBuilder.DropTable(
                name: "WatchLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
