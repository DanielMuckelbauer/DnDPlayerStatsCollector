using Microsoft.EntityFrameworkCore.Migrations;

namespace Roll20Stats.InfrastructureLayer.DAL.Migrations
{
    public partial class ChangeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatistics_Games_GameID",
                table: "PlayerStatistics");

            migrationBuilder.RenameColumn(
                name: "GameID",
                table: "PlayerStatistics",
                newName: "GameId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PlayerStatistics",
                newName: "CharacterName");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerStatistics_GameID",
                table: "PlayerStatistics",
                newName: "IX_PlayerStatistics_GameId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Games",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatistics_Games_GameId",
                table: "PlayerStatistics",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatistics_Games_GameId",
                table: "PlayerStatistics");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "PlayerStatistics",
                newName: "GameID");

            migrationBuilder.RenameColumn(
                name: "CharacterName",
                table: "PlayerStatistics",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerStatistics_GameId",
                table: "PlayerStatistics",
                newName: "IX_PlayerStatistics_GameID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Games",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatistics_Games_GameID",
                table: "PlayerStatistics",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
