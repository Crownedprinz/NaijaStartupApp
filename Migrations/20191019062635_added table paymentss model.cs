using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class addedtablepaymentssmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApiRequest = table.Column<string>(nullable: true),
                    ApiResponse = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    RegistrationId = table.Column<Guid>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    ModificationTime = table.Column<DateTime>(nullable: false),
                    ModificationUserId = table.Column<string>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: false),
                    DeletionUserId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Company_Registration_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Company_Registration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RegistrationId",
                table: "Payments",
                column: "RegistrationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
