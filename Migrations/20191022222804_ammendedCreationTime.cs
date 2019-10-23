using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class ammendedCreationTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "AspNetUsers",
                newName: "CreationTime");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "AspNetUsers",
                newName: "CreatedDate");
        }
    }
}
