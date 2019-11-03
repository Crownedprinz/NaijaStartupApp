using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class addediscacavailable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCacAvailable",
                table: "Company_Registration",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCacAvailable",
                table: "Company_Registration");
        }
    }
}
