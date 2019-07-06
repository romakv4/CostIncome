using Microsoft.EntityFrameworkCore.Migrations;

namespace cost_income_calculator.api.Migrations
{
    public partial class Cascadedeletingcostsandincomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Costs_CostId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Incomes_IncomeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CostId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IncomeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CostId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IncomeId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Incomes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Costs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_UserId",
                table: "Costs",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Users_UserId",
                table: "Costs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Users_UserId",
                table: "Incomes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Users_UserId",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Users_UserId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Costs_UserId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Costs");

            migrationBuilder.AddColumn<int>(
                name: "CostId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncomeId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CostId",
                table: "Users",
                column: "CostId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IncomeId",
                table: "Users",
                column: "IncomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Costs_CostId",
                table: "Users",
                column: "CostId",
                principalTable: "Costs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Incomes_IncomeId",
                table: "Users",
                column: "IncomeId",
                principalTable: "Incomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
