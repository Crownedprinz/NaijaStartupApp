using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class ammended_model_builder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModificationUserId",
                table: "Payments",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationTime",
                table: "Payments",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "Payments",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Payments",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Payments",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldUnicode: false,
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Package",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Company_Registration",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldUnicode: false,
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AddColumn<string>(
                name: "ApprovalStatus",
                table: "Company_Registration",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Company_Officers",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldUnicode: false,
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AddOnService",
                type: "bit",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldUnicode: false,
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "Company_Registration");

            migrationBuilder.AlterColumn<string>(
                name: "ModificationUserId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationTime",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletionTime",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldUnicode: false,
                oldMaxLength: 20,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getutcdate())");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Package",
                type: "bit",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationTime",
                table: "Package",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getutcdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Company_Registration",
                type: "bit",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Company_Officers",
                type: "bit",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AddOnService",
                type: "bit",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "0");
        }
    }
}
