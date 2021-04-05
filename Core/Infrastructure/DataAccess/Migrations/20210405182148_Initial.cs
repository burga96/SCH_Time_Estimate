using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportedBanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportedBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentAmount = table.Column<decimal>(nullable: false),
                    PostalIndexNumber = table.Column<string>(nullable: true),
                    UniqueMasterCitizenNumber_Value = table.Column<string>(nullable: true),
                    PersonalData_FirstName = table.Column<string>(nullable: true),
                    PersonalData_LastName = table.Column<string>(nullable: true),
                    SupportedBankId = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_SupportedBanks_SupportedBankId",
                        column: x => x.SupportedBankId,
                        principalTable: "SupportedBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    PaymentTransactionType = table.Column<string>(nullable: false),
                    InternalTransferId = table.Column<string>(nullable: true),
                    SecondWalletId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Wallets_SecondWalletId",
                        column: x => x.SecondWalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_SecondWalletId",
                table: "PaymentTransactions",
                column: "SecondWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_WalletId",
                table: "PaymentTransactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_SupportedBankId",
                table: "Wallets",
                column: "SupportedBankId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UniqueMasterCitizenNumber_Value",
                table: "Wallets",
                column: "UniqueMasterCitizenNumber_Value",
                unique: true,
                filter: "[UniqueMasterCitizenNumber_Value] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "SupportedBanks");
        }
    }
}