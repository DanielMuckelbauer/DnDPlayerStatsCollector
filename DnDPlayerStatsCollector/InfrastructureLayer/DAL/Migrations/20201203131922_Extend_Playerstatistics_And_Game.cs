using Microsoft.EntityFrameworkCore.Migrations;

namespace Roll20Stats.InfrastructureLayer.DAL.Migrations
{
    public partial class Extend_Playerstatistics_And_Game : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Games");
        }
    }
}
