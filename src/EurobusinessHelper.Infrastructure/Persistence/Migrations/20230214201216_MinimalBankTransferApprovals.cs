using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EurobusinessHelper.Infrastructure.Persistence.Migrations
{
    public partial class MinimalBankTransferApprovals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimalBankTransferApprovals",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimalBankTransferApprovals",
                table: "Games");
        }
    }
}
