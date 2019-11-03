using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class incentiveModelDescriptionChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Incentives",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Description",
                table: "Incentives",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
