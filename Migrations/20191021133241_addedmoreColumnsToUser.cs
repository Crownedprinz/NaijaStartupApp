using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class addedmoreColumnsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                unicode: false,
                maxLength: 1000,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                unicode: false,
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                unicode: false,
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                unicode: false,
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AspNetUsers",
                unicode: false,
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");
        }
    }
}
