using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EurobusinessHelper.Infrastructure.Persistence.Migrations
{
    public partial class AddGameStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Games");
        }
    }
}
