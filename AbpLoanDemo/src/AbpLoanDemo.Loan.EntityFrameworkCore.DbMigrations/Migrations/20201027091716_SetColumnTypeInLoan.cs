using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class SetColumnTypeInLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                "ExpiryDate",
                "Guarantee",
                "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                "ExpiryDate",
                "Guarantee",
                "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}