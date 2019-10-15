using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class addcompany_regandofficerstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company_Registration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyType = table.Column<string>(nullable: true),
                    AlternateCompanyName = table.Column<string>(nullable: true),
                    AlternateCompanyType = table.Column<string>(nullable: true),
                    FinancialYearEnd = table.Column<string>(nullable: true),
                    PackageId = table.Column<string>(nullable: true),
                    PackagesId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    BusinessActivity = table.Column<string>(nullable: true),
                    SndBusinessActivity = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    LocalDirector = table.Column<bool>(nullable: false),
                    CompanyCapitalCurrency = table.Column<string>(nullable: true),
                    NoOfSharesIssue = table.Column<int>(nullable: false),
                    SharePrice = table.Column<decimal>(nullable: false),
                    SharesAllocated = table.Column<decimal>(nullable: false),
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
                    table.PrimaryKey("PK_Company_Registration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Registration_Package_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Package",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Registration_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company_Officers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RegistrationId = table.Column<string>(nullable: true),
                    RegistrationId1 = table.Column<Guid>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    Id_Type = table.Column<string>(nullable: true),
                    Id_Number = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Birth_Country = table.Column<string>(nullable: true),
                    Phone_No = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    PassportFile = table.Column<byte[]>(nullable: true),
                    AddressFile = table.Column<byte[]>(nullable: true),
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
                    table.PrimaryKey("PK_Company_Officers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Officers_Company_Registration_RegistrationId1",
                        column: x => x.RegistrationId1,
                        principalTable: "Company_Registration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_Officers_RegistrationId1",
                table: "Company_Officers",
                column: "RegistrationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Registration_PackagesId",
                table: "Company_Registration",
                column: "PackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Registration_UserId",
                table: "Company_Registration",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company_Officers");

            migrationBuilder.DropTable(
                name: "Company_Registration");
        }
    }
}
