#pragma warning disable 1591
using Microsoft.EntityFrameworkCore.Migrations;

namespace CostIncomeCalculator.Migrations
{
    public partial class Replacetypebycategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Incomes",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Costs",
                newName: "Category");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Limits",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Limits");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Incomes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Costs",
                newName: "Type");
        }
    }
}
