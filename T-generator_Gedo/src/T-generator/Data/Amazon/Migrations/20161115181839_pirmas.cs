using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tgenerator.Migrations
{
    public partial class pirmas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmazonAccount",
                columns: table => new
                {
                    AmazonAccountID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonAccount", x => x.AmazonAccountID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonColor",
                columns: table => new
                {
                    AmazonColorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonColor", x => x.AmazonColorID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonCountry",
                columns: table => new
                {
                    AmazonCountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonCountry", x => x.AmazonCountryID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonCurrency",
                columns: table => new
                {
                    AmazonCurrencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Currency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonCurrency", x => x.AmazonCurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonSize",
                columns: table => new
                {
                    AmazonSizeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonSize", x => x.AmazonSizeID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonTypes",
                columns: table => new
                {
                    AmazonTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonTypes", x => x.AmazonTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonVariation",
                columns: table => new
                {
                    AmazonVariationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonVariation", x => x.AmazonVariationID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonBrowseNode",
                columns: table => new
                {
                    AmazonBrowseNodeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrowseNode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonBrowseNode", x => x.AmazonBrowseNodeID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonBulletPoint",
                columns: table => new
                {
                    AmazonBulletPointID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BulletPoint = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonBulletPoint", x => x.AmazonBulletPointID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonImageURL",
                columns: table => new
                {
                    AmazonImageURLID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonImageURL", x => x.AmazonImageURLID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonKeyword",
                columns: table => new
                {
                    AmazonKeywordID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Keyword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonKeyword", x => x.AmazonKeywordID);
                });

            migrationBuilder.CreateTable(
                name: "AmazonMarketplace",
                columns: table => new
                {
                    AmazonMarketplaceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmazonCurrencyID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonMarketplace", x => x.AmazonMarketplaceID);
                    table.ForeignKey(
                        name: "FK_AmazonMarketplace_AmazonCurrency_AmazonCurrencyID",
                        column: x => x.AmazonCurrencyID,
                        principalTable: "AmazonCurrency",
                        principalColumn: "AmazonCurrencyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmazonProduct",
                columns: table => new
                {
                    AmazonProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmazonTypeID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonProduct", x => x.AmazonProductID);
                    table.ForeignKey(
                        name: "FK_AmazonProduct_AmazonTypes_AmazonTypeID",
                        column: x => x.AmazonTypeID,
                        principalTable: "AmazonTypes",
                        principalColumn: "AmazonTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeywordAssignment",
                columns: table => new
                {
                    KeywordID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordAssignment", x => new { x.KeywordID, x.ProductID, x.Order });
                    table.ForeignKey(
                        name: "FK_KeywordAssignment_AmazonKeyword_KeywordID",
                        column: x => x.KeywordID,
                        principalTable: "AmazonKeyword",
                        principalColumn: "AmazonKeywordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordAssignment_AmazonProduct_ProductID",
                        column: x => x.ProductID,
                        principalTable: "AmazonProduct",
                        principalColumn: "AmazonProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmazonMarketplace_AmazonCurrencyID",
                table: "AmazonMarketplace",
                column: "AmazonCurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_AmazonProduct_AmazonTypeID",
                table: "AmazonProduct",
                column: "AmazonTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordAssignment_KeywordID",
                table: "KeywordAssignment",
                column: "KeywordID");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordAssignment_ProductID",
                table: "KeywordAssignment",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmazonAccount");

            migrationBuilder.DropTable(
                name: "AmazonColor");

            migrationBuilder.DropTable(
                name: "AmazonCountry");

            migrationBuilder.DropTable(
                name: "AmazonSize");

            migrationBuilder.DropTable(
                name: "AmazonVariation");

            migrationBuilder.DropTable(
                name: "AmazonBrowseNode");

            migrationBuilder.DropTable(
                name: "AmazonBulletPoint");

            migrationBuilder.DropTable(
                name: "AmazonImageURL");

            migrationBuilder.DropTable(
                name: "AmazonMarketplace");

            migrationBuilder.DropTable(
                name: "KeywordAssignment");

            migrationBuilder.DropTable(
                name: "AmazonCurrency");

            migrationBuilder.DropTable(
                name: "AmazonKeyword");

            migrationBuilder.DropTable(
                name: "AmazonProduct");

            migrationBuilder.DropTable(
                name: "AmazonTypes");
        }
    }
}
