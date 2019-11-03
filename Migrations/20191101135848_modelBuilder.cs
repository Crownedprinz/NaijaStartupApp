using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class modelBuilder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModificationUserId",
                table: "Incentives",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationTime",
                table: "Incentives",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Incentives",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "DeletionUserId",
                table: "Incentives",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "Incentives",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Incentives",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Incentives",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModificationUserId",
                table: "Incentives",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationTime",
                table: "Incentives",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Incentives",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "DeletionUserId",
                table: "Incentives",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "Incentives",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Incentives",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Incentives",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");
        }
    }
}
