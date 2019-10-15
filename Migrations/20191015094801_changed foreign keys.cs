using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class changedforeignkeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Officers_Company_Registration_RegistrationId1",
                table: "Company_Officers");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Registration_Package_PackageId1",
                table: "Company_Registration");

            migrationBuilder.DropIndex(
                name: "IX_Company_Registration_PackageId1",
                table: "Company_Registration");

            migrationBuilder.DropIndex(
                name: "IX_Company_Officers_RegistrationId1",
                table: "Company_Officers");

            migrationBuilder.DropColumn(
                name: "PackageId1",
                table: "Company_Registration");

            migrationBuilder.DropColumn(
                name: "RegistrationId1",
                table: "Company_Officers");

            migrationBuilder.AlterColumn<Guid>(
                name: "PackageId",
                table: "Company_Registration",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RegistrationId",
                table: "Company_Officers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Registration_PackageId",
                table: "Company_Registration",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Officers_RegistrationId",
                table: "Company_Officers",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Officers_Company_Registration_RegistrationId",
                table: "Company_Officers",
                column: "RegistrationId",
                principalTable: "Company_Registration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Registration_Package_PackageId",
                table: "Company_Registration",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Officers_Company_Registration_RegistrationId",
                table: "Company_Officers");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Registration_Package_PackageId",
                table: "Company_Registration");

            migrationBuilder.DropIndex(
                name: "IX_Company_Registration_PackageId",
                table: "Company_Registration");

            migrationBuilder.DropIndex(
                name: "IX_Company_Officers_RegistrationId",
                table: "Company_Officers");

            migrationBuilder.AlterColumn<string>(
                name: "PackageId",
                table: "Company_Registration",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PackageId1",
                table: "Company_Registration",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationId",
                table: "Company_Officers",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegistrationId1",
                table: "Company_Officers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_Registration_PackageId1",
                table: "Company_Registration",
                column: "PackageId1");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Officers_RegistrationId1",
                table: "Company_Officers",
                column: "RegistrationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Officers_Company_Registration_RegistrationId1",
                table: "Company_Officers",
                column: "RegistrationId1",
                principalTable: "Company_Registration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Registration_Package_PackageId1",
                table: "Company_Registration",
                column: "PackageId1",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
