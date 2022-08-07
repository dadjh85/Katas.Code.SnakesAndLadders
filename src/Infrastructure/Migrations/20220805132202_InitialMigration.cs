using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Board",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TotalBoxes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnakeAndLader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StartBox = table.Column<int>(type: "int", nullable: false),
                    EndBox = table.Column<int>(type: "int", nullable: false),
                    IsLadder = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnakeAndLader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardsSnakesAndLaders",
                columns: table => new
                {
                    BoardsId = table.Column<int>(type: "int", nullable: false),
                    SnakesAndLadersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardsSnakesAndLaders", x => new { x.BoardsId, x.SnakesAndLadersId });
                    table.ForeignKey(
                        name: "FK_BoardsSnakesAndLaders_Board_BoardsId",
                        column: x => x.BoardsId,
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardsSnakesAndLaders_SnakeAndLader_SnakesAndLadersId",
                        column: x => x.SnakesAndLadersId,
                        principalTable: "SnakeAndLader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardsSnakesAndLaders_SnakesAndLadersId",
                table: "BoardsSnakesAndLaders",
                column: "SnakesAndLadersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardsSnakesAndLaders");

            migrationBuilder.DropTable(
                name: "Board");

            migrationBuilder.DropTable(
                name: "SnakeAndLader");
        }
    }
}
