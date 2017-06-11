using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using T_generator.Data;

namespace Tgenerator.Migrations.Amazon
{
    [DbContext(typeof(AmazonContext))]
    [Migration("20170603114741_ExpandedMarketplaceWithColumns")]
    partial class ExpandedMarketplaceWithColumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonAccount", b =>
                {
                    b.Property<int>("AmazonAccountID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonAccountID");

                    b.ToTable("AmazonAccount");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonColor", b =>
                {
                    b.Property<int>("AmazonColorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonColorID");

                    b.ToTable("AmazonColor");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonCountry", b =>
                {
                    b.Property<int>("AmazonCountryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonCountryID");

                    b.ToTable("AmazonCountry");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonCurrency", b =>
                {
                    b.Property<int>("AmazonCurrencyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Currency");

                    b.HasKey("AmazonCurrencyID");

                    b.ToTable("AmazonCurrency");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonDesign", b =>
                {
                    b.Property<int>("AmazonDesignID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmazonAccountID");

                    b.Property<int>("AmazonCategoryID");

                    b.Property<int>("AmazonMarketplaceID");

                    b.Property<string>("DesignURL");

                    b.Property<string>("Name");

                    b.Property<string>("SearchTags1");

                    b.Property<string>("SearchTags2");

                    b.Property<string>("SearchTags3");

                    b.HasKey("AmazonDesignID");

                    b.HasIndex("AmazonAccountID");

                    b.HasIndex("AmazonCategoryID");

                    b.HasIndex("AmazonMarketplaceID");

                    b.ToTable("AmazonDesign");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonSize", b =>
                {
                    b.Property<int>("AmazonSizeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonSizeID");

                    b.ToTable("AmazonSize");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonType", b =>
                {
                    b.Property<int>("AmazonTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonTypeID");

                    b.ToTable("AmazonTypes");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonVariation", b =>
                {
                    b.Property<int>("AmazonVariationID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonVariationID");

                    b.ToTable("AmazonVariation");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonBrowseNode", b =>
                {
                    b.Property<int>("AmazonBrowseNodeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrowseNode");

                    b.HasKey("AmazonBrowseNodeID");

                    b.ToTable("AmazonBrowseNode");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonBulletPoint", b =>
                {
                    b.Property<int>("AmazonBulletPointID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AmazonItemID");

                    b.Property<string>("BulletPoint");

                    b.HasKey("AmazonBulletPointID");

                    b.HasIndex("AmazonItemID");

                    b.ToTable("AmazonBulletPoint");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonCategory", b =>
                {
                    b.Property<int>("AmazonCategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.HasKey("AmazonCategoryID");

                    b.ToTable("AmazonCategory");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonImageURL", b =>
                {
                    b.Property<int>("AmazonImageURLID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AmazonItemID");

                    b.Property<string>("ImageURL");

                    b.HasKey("AmazonImageURLID");

                    b.HasIndex("AmazonItemID");

                    b.ToTable("AmazonImageURL");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonKeyword", b =>
                {
                    b.Property<int>("AmazonKeywordID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AmazonItemID");

                    b.Property<string>("Keyword");

                    b.HasKey("AmazonKeywordID");

                    b.HasIndex("AmazonItemID");

                    b.ToTable("AmazonKeyword");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonListing", b =>
                {
                    b.Property<int>("AmazonListingID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmazonDesignID");

                    b.Property<int>("AmazonProductColorID");

                    b.Property<int>("AmazonProductID");

                    b.Property<string>("DesignURL");

                    b.Property<int>("Left");

                    b.Property<int>("Right");

                    b.Property<int>("Top");

                    b.HasKey("AmazonListingID");

                    b.HasIndex("AmazonDesignID");

                    b.HasIndex("AmazonProductColorID");

                    b.HasIndex("AmazonProductID");

                    b.ToTable("AmazonListing");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonMarketplace", b =>
                {
                    b.Property<int>("AmazonMarketplaceID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdultFlag");

                    b.Property<int>("AmazonCurrencyID");

                    b.Property<int?>("BikiniTopStyle");

                    b.Property<int?>("BraBandSize");

                    b.Property<int?>("BraBandSizeUnit");

                    b.Property<int?>("BraCupSize");

                    b.Property<int?>("BrandName");

                    b.Property<int?>("BrowseNode");

                    b.Property<int?>("CanBeGiftMessaged");

                    b.Property<int?>("CanBeGiftWrapped");

                    b.Property<int?>("ClosureType");

                    b.Property<int?>("ClothingType");

                    b.Property<int?>("CollarStyle");

                    b.Property<int?>("Colour");

                    b.Property<int?>("ColourMap");

                    b.Property<int?>("CountryOfOrigin");

                    b.Property<int?>("Department");

                    b.Property<int?>("Features1");

                    b.Property<int?>("Features2");

                    b.Property<int?>("Features3");

                    b.Property<int?>("Features4");

                    b.Property<int?>("Features5");

                    b.Property<int?>("FittingType");

                    b.Property<int?>("FulfillmentCentreId");

                    b.Property<int?>("FulfillmentLatency");

                    b.Property<int?>("GtinExemptionReason");

                    b.Property<int?>("InnerMaterialType");

                    b.Property<int?>("IsDiscontinued");

                    b.Property<int?>("ItemLength");

                    b.Property<int?>("ItemSKU");

                    b.Property<int?>("ItemShape");

                    b.Property<int?>("JeansLengthInches");

                    b.Property<int?>("JeansLengthUnitOfMeasure");

                    b.Property<int?>("JeansWidthInches");

                    b.Property<int?>("JeansWidthUnitOfMeasure");

                    b.Property<int?>("LaunchDate");

                    b.Property<int?>("MainImgUrl");

                    b.Property<int?>("ManufacturerPartNumber");

                    b.Property<int?>("MaterialComposition");

                    b.Property<int?>("MaxAggrShipQuant");

                    b.Property<int?>("MerchantShippingGroup");

                    b.Property<int?>("ModelName");

                    b.Property<int?>("ModelNumber");

                    b.Property<string>("Name");

                    b.Property<int?>("NeckStyle");

                    b.Property<int?>("NumberOfItems");

                    b.Property<int?>("OccasionDescription");

                    b.Property<int?>("OpacityTransparency");

                    b.Property<int?>("OtherImgUrl");

                    b.Property<int?>("OuterMaterialType");

                    b.Property<int?>("PackageDimensionsUnitOfMeasure");

                    b.Property<int?>("PackageHeight");

                    b.Property<int?>("PackageLength");

                    b.Property<int?>("PackageLengthUnitOfMeasure");

                    b.Property<int?>("PackageQuantity");

                    b.Property<int?>("PackageWeight");

                    b.Property<int?>("PackageWeightUnitOfMeasure");

                    b.Property<int?>("PackageWidth");

                    b.Property<int?>("ParentSKU");

                    b.Property<int?>("Parentage");

                    b.Property<int?>("PatternDescription");

                    b.Property<string>("Prefix");

                    b.Property<int?>("ProductCareInstructions");

                    b.Property<int?>("ProductDescription");

                    b.Property<int?>("ProductID");

                    b.Property<int?>("ProductName");

                    b.Property<int?>("ProductType");

                    b.Property<int?>("Quantity");

                    b.Property<int?>("RelatedProductID");

                    b.Property<int?>("RelatedProductType");

                    b.Property<int?>("RelationshipType");

                    b.Property<int?>("SaleEndDate");

                    b.Property<int?>("SaleFromDate");

                    b.Property<int?>("SalePrice");

                    b.Property<int?>("SearchTerms");

                    b.Property<int?>("SeasonAndCollectionYear");

                    b.Property<int>("SheetNumber");

                    b.Property<int?>("ShippingWeight");

                    b.Property<int?>("Size");

                    b.Property<int?>("SizeMap");

                    b.Property<int?>("SleeveType");

                    b.Property<int?>("SpecialFeatures");

                    b.Property<int?>("StandardPrice");

                    b.Property<int>("StartingRow");

                    b.Property<int?>("StyleName");

                    b.Property<int?>("SwatchImgUrl");

                    b.Property<int?>("SwimwearBottomStyle");

                    b.Property<string>("TemplateURL");

                    b.Property<int?>("UpdateDelete");

                    b.Property<int?>("VariationTheme");

                    b.Property<int?>("WeightUnitOfMeasure");

                    b.HasKey("AmazonMarketplaceID");

                    b.HasIndex("AmazonCurrencyID");

                    b.ToTable("AmazonMarketplace");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", b =>
                {
                    b.Property<int>("AmazonProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmazonAccountID");

                    b.Property<int>("AmazonTypeID");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("Prefix");

                    b.HasKey("AmazonProductID");

                    b.HasIndex("AmazonAccountID");

                    b.HasIndex("AmazonTypeID");

                    b.ToTable("AmazonProduct");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonProductColor", b =>
                {
                    b.Property<int>("AmazonProductColorID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmazonProductID");

                    b.Property<string>("DesignURL");

                    b.Property<string>("Name");

                    b.Property<decimal>("Opacity");

                    b.HasKey("AmazonProductColorID");

                    b.HasIndex("AmazonProductID");

                    b.ToTable("AmazonProductColor");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.JoinTables.AccountMarketplaces", b =>
                {
                    b.Property<int>("MarketplaceID");

                    b.Property<int>("AccountID");

                    b.HasKey("MarketplaceID", "AccountID");

                    b.HasIndex("AccountID");

                    b.HasIndex("MarketplaceID");

                    b.ToTable("AccountMarketplaces");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.JoinTables.KeywordAssignment", b =>
                {
                    b.Property<int>("KeywordID");

                    b.Property<int>("ProductID");

                    b.Property<int>("Order");

                    b.HasKey("KeywordID", "ProductID", "Order");

                    b.HasIndex("KeywordID");

                    b.HasIndex("ProductID");

                    b.ToTable("KeywordAssignment");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.JoinTables.ProductSizes", b =>
                {
                    b.Property<int>("SizeID");

                    b.Property<int>("ProductID");

                    b.HasKey("SizeID", "ProductID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SizeID");

                    b.ToTable("ProductSizes");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Objects.Item.AmazonItem", b =>
                {
                    b.Property<int>("AmazonItemID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmazonBrowseNodeID");

                    b.Property<int>("AmazonCountryID");

                    b.Property<int>("AmazonProductID");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("AmazonItemID");

                    b.HasIndex("AmazonBrowseNodeID");

                    b.HasIndex("AmazonCountryID");

                    b.HasIndex("AmazonProductID");

                    b.ToTable("AmazonItem");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AmazonItem");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Objects.Item.Children.AmazonItemSingle", b =>
                {
                    b.HasBaseType("T_generator.Models.Amazon.Objects.Item.AmazonItem");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.ToTable("AmazonItemSingle");

                    b.HasDiscriminator().HasValue("AmazonItemSingle");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Basic.AmazonDesign", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonAccount", "AmazonAccount")
                        .WithMany()
                        .HasForeignKey("AmazonAccountID");

                    b.HasOne("T_generator.Models.Amazon.Data.Dump.AmazonCategory", "AmazonCategory")
                        .WithMany()
                        .HasForeignKey("AmazonCategoryID");

                    b.HasOne("T_generator.Models.Amazon.Data.Intermediate.AmazonMarketplace", "AmazonMarketplace")
                        .WithMany()
                        .HasForeignKey("AmazonMarketplaceID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonBulletPoint", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Objects.Item.AmazonItem")
                        .WithMany("AmazonBulletPoint")
                        .HasForeignKey("AmazonItemID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonImageURL", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Objects.Item.AmazonItem")
                        .WithMany("AmazonImageURL")
                        .HasForeignKey("AmazonItemID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Dump.AmazonKeyword", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Objects.Item.AmazonItem")
                        .WithMany("AmazonKeyword")
                        .HasForeignKey("AmazonItemID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonListing", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonDesign", "AmazonDesign")
                        .WithMany()
                        .HasForeignKey("AmazonDesignID");

                    b.HasOne("T_generator.Models.Amazon.Data.Intermediate.AmazonProductColor", "AmazonProductColor")
                        .WithMany()
                        .HasForeignKey("AmazonProductColorID");

                    b.HasOne("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", "AmazonProduct")
                        .WithMany()
                        .HasForeignKey("AmazonProductID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonMarketplace", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonCurrency", "AmazonCurrency")
                        .WithMany()
                        .HasForeignKey("AmazonCurrencyID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonAccount", "AmazonAccount")
                        .WithMany()
                        .HasForeignKey("AmazonAccountID");

                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonType", "AmazonType")
                        .WithMany()
                        .HasForeignKey("AmazonTypeID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.Intermediate.AmazonProductColor", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", "AmazonProduct")
                        .WithMany()
                        .HasForeignKey("AmazonProductID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.JoinTables.AccountMarketplaces", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonAccount", "Account")
                        .WithMany("Marketplaces")
                        .HasForeignKey("AccountID");

                    b.HasOne("T_generator.Models.Amazon.Data.Intermediate.AmazonMarketplace", "Marketplace")
                        .WithMany()
                        .HasForeignKey("MarketplaceID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.JoinTables.KeywordAssignment", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Dump.AmazonKeyword", "Keyword")
                        .WithMany()
                        .HasForeignKey("KeywordID");

                    b.HasOne("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", "Product")
                        .WithMany("Keywords")
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Data.JoinTables.ProductSizes", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", "Product")
                        .WithMany("Sizes")
                        .HasForeignKey("ProductID");

                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonSize", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID");
                });

            modelBuilder.Entity("T_generator.Models.Amazon.Objects.Item.AmazonItem", b =>
                {
                    b.HasOne("T_generator.Models.Amazon.Data.Dump.AmazonBrowseNode", "AmazonBrowseNode")
                        .WithMany()
                        .HasForeignKey("AmazonBrowseNodeID");

                    b.HasOne("T_generator.Models.Amazon.Data.Basic.AmazonCountry", "AmazonCountry")
                        .WithMany()
                        .HasForeignKey("AmazonCountryID");

                    b.HasOne("T_generator.Models.Amazon.Data.Intermediate.AmazonProduct", "AmazonProduct")
                        .WithMany()
                        .HasForeignKey("AmazonProductID");
                });
        }
    }
}
