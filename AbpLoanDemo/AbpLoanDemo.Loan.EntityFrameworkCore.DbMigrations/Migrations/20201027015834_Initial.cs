using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbpLoanDemo.Loan.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guarantee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guarantee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Score = table.Column<decimal>(nullable: false),
                    GuaranteeId = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanRequest_Guarantee_GuaranteeId",
                        column: x => x.GuaranteeId,
                        principalTable: "Guarantee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Applier",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LoanRequestId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applier_LoanRequest_LoanRequestId",
                        column: x => x.LoanRequestId,
                        principalTable: "LoanRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applier_LoanRequestId",
                table: "Applier",
                column: "LoanRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequest_GuaranteeId",
                table: "LoanRequest",
                column: "GuaranteeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applier");

            migrationBuilder.DropTable(
                name: "LoanRequest");

            migrationBuilder.DropTable(
                name: "Guarantee");
        }
    }
}
