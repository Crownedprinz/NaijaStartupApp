using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class addedmorecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShareHolderName",
                table: "Company_Registration",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dob",
                table: "Company_Officers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Company_Officers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShareHolderName",
                table: "Company_Registration");

            migrationBuilder.DropColumn(
                name: "Dob",
                table: "Company_Officers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Company_Officers");
        }
    }
}
