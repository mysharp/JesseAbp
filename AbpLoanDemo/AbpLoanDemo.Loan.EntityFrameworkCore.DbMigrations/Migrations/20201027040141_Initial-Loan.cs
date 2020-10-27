using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class InitialLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Guarantee",
                table => new
                {
                    Id = table.Column<Guid>(),
                    Name = table.Column<string>(maxLength: 100),
                    Cost = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    ExpiryDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Guarantee", x => x.Id); });

            migrationBuilder.CreateTable(
                "LoanRequest",
                table => new
                {
                    Id = table.Column<Guid>(),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    ApplierId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(),
                    Score = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    GuaranteeId = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRequest", x => x.Id);
                    table.ForeignKey(
                        "FK_LoanRequest_Guarantee_GuaranteeId",
                        x => x.GuaranteeId,
                        "Guarantee",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Applier",
                table => new
                {
                    Id = table.Column<Guid>(),
                    CustomerId = table.Column<Guid>(),
                    Name = table.Column<string>(maxLength: 100),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    IdNo = table.Column<string>(maxLength: 50, nullable: true),
                    LoanRequestId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applier", x => x.Id);
                    table.ForeignKey(
                        "FK_Applier_LoanRequest_LoanRequestId",
                        x => x.LoanRequestId,
                        "LoanRequest",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Applier_LoanRequestId",
                "Applier",
                "LoanRequestId");

            migrationBuilder.CreateIndex(
                "IX_LoanRequest_ApplierId",
                "LoanRequest",
                "ApplierId");

            migrationBuilder.CreateIndex(
                "IX_LoanRequest_GuaranteeId",
                "LoanRequest",
                "GuaranteeId");

            migrationBuilder.AddForeignKey(
                "FK_LoanRequest_Applier_ApplierId",
                "LoanRequest",
                "ApplierId",
                "Applier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Applier_LoanRequest_LoanRequestId",
                "Applier");

            migrationBuilder.DropTable(
                "LoanRequest");

            migrationBuilder.DropTable(
                "Applier");

            migrationBuilder.DropTable(
                "Guarantee");
        }
    }
}