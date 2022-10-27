using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EurobusinessHelper.Infrastructure.Persistence.Migrations
{
    public partial class EntitiesTimestamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Identities",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CI_AI",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Identities",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CI_AI",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Identities",
                type: "nvarchar(260)",
                maxLength: 260,
                nullable: false,
                collation: "SQL_Latin1_General_CP1_CI_AI",
                oldClrType: typeof(string),
                oldType: "nvarchar(260)",
                oldMaxLength: 260,
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Identities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Identities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Games",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "SQL_Latin1_General_CP1_CI_AI",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Identities");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Identities");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Account");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Identities",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldCollation: "SQL_Latin1_General_CP1_CI_AI");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Identities",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldCollation: "SQL_Latin1_General_CP1_CI_AI");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Identities",
                type: "nvarchar(260)",
                maxLength: 260,
                nullable: false,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(260)",
                oldMaxLength: 260,
                oldCollation: "SQL_Latin1_General_CP1_CI_AI");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Games",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldCollation: "SQL_Latin1_General_CP1_CI_AI");
        }
    }
}
