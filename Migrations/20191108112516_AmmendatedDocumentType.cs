using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class AmmendatedDocumentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassportFile",
                table: "Company_Officers",
                newName: "Proficiency");

            migrationBuilder.RenameColumn(
                name: "AddressFile",
                table: "Company_Officers",
                newName: "Identification");

            migrationBuilder.AddColumn<byte[]>(
                name: "CerficationOfBirth",
                table: "Company_Officers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CerficationOfBirth",
                table: "Company_Officers");

            migrationBuilder.RenameColumn(
                name: "Proficiency",
                table: "Company_Officers",
                newName: "PassportFile");

            migrationBuilder.RenameColumn(
                name: "Identification",
                table: "Company_Officers",
                newName: "AddressFile");
        }
    }
}
