using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class changedpackagestopackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Registration_Package_PackagesId",
                table: "Company_Registration");

            migrationBuilder.RenameColumn(
                name: "PackagesId",
                table: "Company_Registration",
                newName: "PackageId1");

            migrationBuilder.RenameIndex(
                name: "IX_Company_Registration_PackagesId",
                table: "Company_Registration",
                newName: "IX_Company_Registration_PackageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Registration_Package_PackageId1",
                table: "Company_Registration",
                column: "PackageId1",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Registration_Package_PackageId1",
                table: "Company_Registration");

            migrationBuilder.RenameColumn(
                name: "PackageId1",
                table: "Company_Registration",
                newName: "PackagesId");

            migrationBuilder.RenameIndex(
                name: "IX_Company_Registration_PackageId1",
                table: "Company_Registration",
                newName: "IX_Company_Registration_PackagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Registration_Package_PackagesId",
                table: "Company_Registration",
                column: "PackagesId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
