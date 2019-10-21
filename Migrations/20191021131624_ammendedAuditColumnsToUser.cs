using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class ammendedAuditColumnsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getutcdate())");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "AspNetUsers",
                unicode: false,
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getutcdate())");

            migrationBuilder.AddColumn<string>(
                name: "DeletionUserId",
                table: "AspNetUsers",
                unicode: false,
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationTime",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getutcdate())");

            migrationBuilder.AddColumn<string>(
                name: "ModificationUserId",
                table: "AspNetUsers",
                unicode: false,
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletionUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModificationTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModificationUserId",
                table: "AspNetUsers");
        }
    }
}
