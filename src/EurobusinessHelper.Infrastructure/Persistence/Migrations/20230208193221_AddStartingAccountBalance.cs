using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EurobusinessHelper.Infrastructure.Persistence.Migrations
{
    public partial class AddStartingAccountBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StartingAccountBalance",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingAccountBalance",
                table: "Games");
        }
    }
}
