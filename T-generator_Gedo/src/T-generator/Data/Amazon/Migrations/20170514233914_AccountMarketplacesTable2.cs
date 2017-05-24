using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tgenerator.Migrations.Amazon
{
    public partial class AccountMarketplacesTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "AccountMarketplaces",
                columns: table => new
                {
                    MarketplaceID = table.Column<int>(nullable: false),
                    AccountID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMarketplaces", x => new { x.MarketplaceID, x.AccountID });
                    table.ForeignKey(
                        name: "FK_AccountMarketplaces_AmazonMarketplace_MarketplaceID",
                        column: x => x.MarketplaceID,
                        principalTable: "AmazonMarketplace",
                        principalColumn: "AmazonMarketplaceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountMarketplaces_AmazonAccount_AccountID",
                        column: x => x.AccountID,
                        principalTable: "AmazonAccount",
                        principalColumn: "AmazonAccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountMarketplaces_MarketplaceID",
                table: "AccountMarketplaces",
                column: "MarketplaceID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountMarketplaces_AccountID",
                table: "AccountMarketplaces",
                column: "AccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                 migrationBuilder.DropTable(
                name: "AccountMarketplaces");
                }
    }
}
