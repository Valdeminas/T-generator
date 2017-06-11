using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tgenerator.Migrations.Amazon
{
    public partial class ExpandedMarketplaceWithColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdultFlag",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BikiniTopStyle",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BraBandSize",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BraBandSizeUnit",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BraCupSize",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandName",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrowseNode",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CanBeGiftMessaged",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CanBeGiftWrapped",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClosureType",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClothingType",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollarStyle",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Colour",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColourMap",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryOfOrigin",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Features1",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Features2",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Features3",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Features4",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Features5",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FittingType",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FulfillmentCentreId",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FulfillmentLatency",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GtinExemptionReason",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InnerMaterialType",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsDiscontinued",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemLength",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemSKU",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemShape",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JeansLengthInches",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JeansLengthUnitOfMeasure",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JeansWidthInches",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JeansWidthUnitOfMeasure",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaunchDate",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainImgUrl",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerPartNumber",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialComposition",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxAggrShipQuant",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MerchantShippingGroup",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModelName",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModelNumber",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NeckStyle",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfItems",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OccasionDescription",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpacityTransparency",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OtherImgUrl",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OuterMaterialType",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageDimensionsUnitOfMeasure",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageHeight",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageLength",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageLengthUnitOfMeasure",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageQuantity",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageWeight",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageWeightUnitOfMeasure",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageWidth",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentSKU",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Parentage",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatternDescription",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductCareInstructions",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductDescription",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductName",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelatedProductID",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelatedProductType",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelationshipType",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleEndDate",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleFromDate",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalePrice",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SearchTerms",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeasonAndCollectionYear",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShippingWeight",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeMap",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SleeveType",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialFeatures",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StandardPrice",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StyleName",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SwatchImgUrl",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SwimwearBottomStyle",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateDelete",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VariationTheme",
                table: "AmazonMarketplace",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeightUnitOfMeasure",
                table: "AmazonMarketplace",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultFlag",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "BikiniTopStyle",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "BraBandSize",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "BraBandSizeUnit",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "BraCupSize",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "BrowseNode",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "CanBeGiftMessaged",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "CanBeGiftWrapped",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ClosureType",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ClothingType",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "CollarStyle",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Colour",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ColourMap",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Features1",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Features2",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Features3",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Features4",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Features5",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "FittingType",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "FulfillmentCentreId",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "FulfillmentLatency",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "GtinExemptionReason",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "InnerMaterialType",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "IsDiscontinued",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ItemLength",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ItemSKU",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ItemShape",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "JeansLengthInches",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "JeansLengthUnitOfMeasure",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "JeansWidthInches",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "JeansWidthUnitOfMeasure",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "LaunchDate",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "MainImgUrl",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ManufacturerPartNumber",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "MaterialComposition",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "MaxAggrShipQuant",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "MerchantShippingGroup",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ModelName",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ModelNumber",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "NeckStyle",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "NumberOfItems",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "OccasionDescription",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "OpacityTransparency",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "OtherImgUrl",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "OuterMaterialType",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "PackageDimensionsUnitOfMeasure",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "PackageHeight",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "PackageLength",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "PackageLengthUnitOfMeasure",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "PackageQuantity",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "PackageWeight",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "PackageWeightUnitOfMeasure",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "PackageWidth",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ParentSKU",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Parentage",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "PatternDescription",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ProductCareInstructions",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "RelatedProductID",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "RelatedProductType",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "RelationshipType",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SaleEndDate",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SaleFromDate",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SearchTerms",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SeasonAndCollectionYear",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "ShippingWeight",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SizeMap",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SleeveType",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SpecialFeatures",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "StandardPrice",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "StyleName",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SwatchImgUrl",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "SwimwearBottomStyle",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "UpdateDelete",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "VariationTheme",
                table: "AmazonMarketplace");

            migrationBuilder.DropColumn(
                name: "WeightUnitOfMeasure",
                table: "AmazonMarketplace");
        }
    }
}
