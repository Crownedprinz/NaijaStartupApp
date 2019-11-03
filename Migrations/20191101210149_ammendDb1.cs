using Microsoft.EntityFrameworkCore.Migrations;

namespace NaijaStartupApp.Migrations
{
    public partial class ammendDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IncentiveId",
                table: "Comp_Incentives",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Comp_Incentives_IncentiveId",
                table: "Comp_Incentives",
                column: "IncentiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comp_Incentives_Incentives_IncentiveId",
                table: "Comp_Incentives",
                column: "IncentiveId",
                principalTable: "Incentives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comp_Incentives_Incentives_IncentiveId",
                table: "Comp_Incentives");

            migrationBuilder.DropIndex(
                name: "IX_Comp_Incentives_IncentiveId",
                table: "Comp_Incentives");

            migrationBuilder.AlterColumn<int>(
                name: "IncentiveId",
                table: "Comp_Incentives",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
