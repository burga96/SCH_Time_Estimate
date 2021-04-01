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
                    UniqueMasterCitizenNumber_Value = table.Column<string>(nullable: true),
                    PersonalData_FirstName = table.Column<string>(nullable: true),
                    PersonalData_LastName = table.Column<string>(nullable: true),
                    SupportedBankId = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: false)
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
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "SupportedBanks");
        }
    }
}
