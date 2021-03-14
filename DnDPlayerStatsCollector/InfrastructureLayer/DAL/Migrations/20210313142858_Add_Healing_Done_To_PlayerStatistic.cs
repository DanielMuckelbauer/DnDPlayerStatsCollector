using Microsoft.EntityFrameworkCore.Migrations;

namespace Roll20Stats.InfrastructureLayer.DAL.Migrations
{
    public partial class Add_Healing_Done_To_PlayerStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealingDone",
                table: "PlayerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealingDone",
                table: "PlayerStatistics");
        }
    }
}
