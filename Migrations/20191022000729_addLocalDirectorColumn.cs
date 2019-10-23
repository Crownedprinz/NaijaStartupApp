using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class addLocalDirectorColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LocalResidentDirector",
                table: "Company_Registration",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "LocalResidentDirectorPrice",
                table: "Company_Registration",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalResidentDirector",
                table: "Company_Registration");

            migrationBuilder.DropColumn(
                name: "LocalResidentDirectorPrice",
                table: "Company_Registration");
        }
    }
}
