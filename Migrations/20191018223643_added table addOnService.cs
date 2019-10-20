using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class addedtableaddOnService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddOnService",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistrationId = table.Column<Guid>(nullable: true),
                    ServiceName = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    Price = table.Column<decimal>(nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatorUserId = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    ModificationTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModificationUserId = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    DeletionTime = table.Column<DateTime>(unicode: false, maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    DeletionUserId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOnService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddOnService_Company_Registration_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Company_Registration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddOnService_RegistrationId",
                table: "AddOnService",
                column: "RegistrationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddOnService");
        }
    }
}
