using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tgenerator.Migrations.Amazon
{
    public partial class ProductColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmazonProductColor",
                columns: table => new
                {
                    AmazonProductColorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmazonProductID = table.Column<int>(nullable: false),
                    DesignURL = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Opacity = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazonProductColor", x => x.AmazonProductColorID);
                    table.ForeignKey(
                        name: "FK_AmazonProductColor_AmazonProduct_AmazonProductID",
                        column: x => x.AmazonProductID,
                        principalTable: "AmazonProduct",
                        principalColumn: "AmazonProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmazonProductColor_AmazonProductID",
                table: "AmazonProductColor",
                column: "AmazonProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmazonProductColor");
        }
    }
}
