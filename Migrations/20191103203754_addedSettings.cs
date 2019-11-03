using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class addedSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    field1 = table.Column<string>(nullable: true),
                    field2 = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatorUserId = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    ModificationTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    ModificationUserId = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    DeletionTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    DeletionUserId = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
