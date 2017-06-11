using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;

namespace T_generator.Models.Amazon.Data.Intermediate
{
    public class AmazonMarketplace
    {
        public int AmazonMarketplaceID { get; set; }

        public string Name { get; set; }
        public string Prefix { get; set; }

        [Display(Name = "Currency")]
        public int AmazonCurrencyID { get; set; }

        public virtual AmazonCurrency AmazonCurrency { get; set; }

        [Display(Name = "Template")]
        public string TemplateURL { get; set; }

        [Display(Name = "Sheet Number")]
        [Range(1, Double.PositiveInfinity)]
        public int SheetNumber { get; set; }

        [Display(Name = "Starting Row")]
        [Range(1, Double.PositiveInfinity)]
        public int StartingRow { get; set; }

        [Display(Name = "Item SKU")]
        [Range(1, Double.PositiveInfinity)]
        public int? ItemSKU { get; set; }

        [Display(Name = "Product ID")]
        [Range(1, Double.PositiveInfinity)]
        public int? ProductID { get; set; }

        [Display(Name = "Product Type")]
        [Range(1, Double.PositiveInfinity)]
        public int? ProductType { get; set; }

        [Display(Name = "Product Name")]
        [Range(1, Double.PositiveInfinity)]
        public int? ProductName { get; set; }

        [Display(Name = "Brand Name")]
        [Range(1, Double.PositiveInfinity)]
        public int? BrandName { get; set; }

        [Display(Name = "Clothing Type")]
        [Range(1, Double.PositiveInfinity)]
        public int? ClothingType { get; set; }

        [Display(Name = "Product Description")]
        [Range(1, Double.PositiveInfinity)]
        public int? ProductDescription { get; set; }

        [Display(Name = "Update/Delete")]
        [Range(1, Double.PositiveInfinity)]
        public int? UpdateDelete { get; set; }

        [Display(Name = "Model Number")]
        [Range(1, Double.PositiveInfinity)]
        public int? ModelNumber { get; set; }

        [Display(Name = "Manufacturer Part Number")]
        [Range(1, Double.PositiveInfinity)]
        public int? ManufacturerPartNumber { get; set; }

        [Display(Name = "Related Product Type")]
        [Range(1, Double.PositiveInfinity)]
        public int? RelatedProductType { get; set; }

        [Display(Name = "Related Product ID")]
        [Range(1, Double.PositiveInfinity)]
        public int? RelatedProductID { get; set; }

        [Display(Name = "Gtin Exemption Reason")]
        [Range(1, Double.PositiveInfinity)]
        public int? GtinExemptionReason { get; set; }

        [Display(Name = "Standard Price")]
        [Range(1, Double.PositiveInfinity)]
        public int? StandardPrice { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, Double.PositiveInfinity)]
        public int? Quantity { get; set; }

        [Display(Name = "Fulfillment Latency")]
        [Range(1, Double.PositiveInfinity)]
        public int? FulfillmentLatency { get; set; }

        [Display(Name = "Sale Price")]
        [Range(1, Double.PositiveInfinity)]
        public int? SalePrice { get; set; }

        [Display(Name = "Sale From Date")]
        [Range(1, Double.PositiveInfinity)]
        public int? SaleFromDate { get; set; }

        [Display(Name = "Sale End Date")]
        [Range(1, Double.PositiveInfinity)]
        public int? SaleEndDate { get; set; }

        [Display(Name = "Max Aggr Ship Quant")]
        [Range(1, Double.PositiveInfinity)]
        public int? MaxAggrShipQuant { get; set; }

        [Display(Name = "Package Quantity")]
        [Range(1, Double.PositiveInfinity)]
        public int? PackageQuantity { get; set; }

        [Display(Name = "Number of Items")]
        [Range(1, Double.PositiveInfinity)]
        public int? NumberOfItems { get; set; }

        [Display(Name = "Can Be Gift Messaged")]
        [Range(1, Double.PositiveInfinity)]
        public int? CanBeGiftMessaged { get; set; }

        [Display(Name = "Can Be Gift Wrapped")]
        [Range(1, Double.PositiveInfinity)]
        public int? CanBeGiftWrapped { get; set; }

        [Display(Name = "Is Discontinued")]
        [Range(1, Double.PositiveInfinity)]
        public int? IsDiscontinued { get; set; }

        [Display(Name = "Launch Date")]
        [Range(1, Double.PositiveInfinity)]
        public int? LaunchDate { get; set; }

        [Display(Name = "Merchant Shipping Group")]
        [Range(1, Double.PositiveInfinity)]
        public int? MerchantShippingGroup { get; set; }

        [Display(Name = "Shipping Weight")]
        [Range(1, Double.PositiveInfinity)]
        public int? ShippingWeight { get; set; }

        [Display(Name = "Weight Unit Of Measure")]
        [Range(1, Double.PositiveInfinity)]
        public int? WeightUnitOfMeasure { get; set; }

        [Display(Name = "Browse Node")]
        [Range(1, Double.PositiveInfinity)]
        public int? BrowseNode { get; set; }

        [Display(Name = "Search Terms")]
        [Range(1, Double.PositiveInfinity)]
        public int? SearchTerms { get; set; }

        [Display(Name = "Features 1")]
        [Range(1, Double.PositiveInfinity)]
        public int? Features1 { get; set; }

        [Display(Name = "Features 2")]
        [Range(1, Double.PositiveInfinity)]
        public int? Features2 { get; set; }

        [Display(Name = "Features 3")]
        [Range(1, Double.PositiveInfinity)]
        public int? Features3 { get; set; }

        [Display(Name = "Features 4")]
        [Range(1, Double.PositiveInfinity)]
        public int? Features4 { get; set; }

        [Display(Name = "Features 5")]
        [Range(1, Double.PositiveInfinity)]
        public int? Features5 { get; set; }

        [Display(Name = "Main Img Url")]
        [Range(1, Double.PositiveInfinity)]
        public int? MainImgUrl { get; set; }

        [Display(Name = "Other Img Url")]
        [Range(1, Double.PositiveInfinity)]
        public int? OtherImgUrl { get; set; }

        [Display(Name = "Swatch Img Url")]
        [Range(1, Double.PositiveInfinity)]
        public int? SwatchImgUrl { get; set; }

        [Display(Name = "Fulfillment Centre Id")]
        [Range(1, Double.PositiveInfinity)]
        public int? FulfillmentCentreId { get; set; }

        [Display(Name = "Package Length")]
        [Range(1, Double.PositiveInfinity)]
        public int? PackageLength { get; set; }

        [Display(Name = "Package Width")]
        [Range(1, Double.PositiveInfinity)]
        public int? PackageWidth { get; set; }

        [Display(Name = "Package Height")]
        [Range(1, Double.PositiveInfinity)]
        public int? PackageHeight { get; set; }

        [Display(Name = "Package Length Unit Of Measure")]
        [Range(1, Double.PositiveInfinity)]
        public int? PackageLengthUnitOfMeasure { get; set; }

        [Display(Name = "Package Weight")]
        [Range(1, Double.PositiveInfinity)]
        public int? PackageWeight { get; set; }

        [Display(Name = "Package Weight Unit Of Measure")]
        [Range(1, Double.PositiveInfinity)]
        public int? PackageWeightUnitOfMeasure { get; set; }

        [Display(Name = "Package Dimensions Unit Of Measure")]
        [Range(1, Double.PositiveInfinity)]
        public int? PackageDimensionsUnitOfMeasure { get; set; }

        [Display(Name = "Parentage")]
        [Range(1, Double.PositiveInfinity)]
        public int? Parentage { get; set; }

        [Display(Name = "Parent SKU")]
        [Range(1, Double.PositiveInfinity)]
        public int? ParentSKU { get; set; }

        [Display(Name = "Relationship Type")]
        [Range(1, Double.PositiveInfinity)]
        public int? RelationshipType { get; set; }

        [Display(Name = "Variation Theme")]
        [Range(1, Double.PositiveInfinity)]
        public int? VariationTheme { get; set; }

        [Display(Name = "Country Of Origin")]
        [Range(1, Double.PositiveInfinity)]
        public int? CountryOfOrigin { get; set; }

        [Display(Name = "Colour Map")]
        [Range(1, Double.PositiveInfinity)]
        public int? ColourMap { get; set; }

        [Display(Name = "Colour")]
        [Range(1, Double.PositiveInfinity)]
        public int? Colour { get; set; }

        [Display(Name = "Size Map")]
        [Range(1, Double.PositiveInfinity)]
        public int? SizeMap { get; set; }

        [Display(Name = "Size")]
        [Range(1, Double.PositiveInfinity)]
        public int? Size { get; set; }

        [Display(Name = "Material Composition")]
        [Range(1, Double.PositiveInfinity)]
        public int? MaterialComposition { get; set; }

        [Display(Name = "Outer Material Type")]
        [Range(1, Double.PositiveInfinity)]
        public int? OuterMaterialType { get; set; }

        [Display(Name = "Inner Material Type")]
        [Range(1, Double.PositiveInfinity)]
        public int? InnerMaterialType { get; set; }

        [Display(Name = "Season And Collection Year")]
        [Range(1, Double.PositiveInfinity)]
        public int? SeasonAndCollectionYear { get; set; }

        [Display(Name = "Product Care Instructions")]
        [Range(1, Double.PositiveInfinity)]
        public int? ProductCareInstructions { get; set; }

        [Display(Name = "Model Name")]
        [Range(1, Double.PositiveInfinity)]
        public int? ModelName { get; set; }

        [Display(Name = "Department")]
        [Range(1, Double.PositiveInfinity)]
        public int? Department { get; set; }

        [Display(Name = "Adult Flag")]
        [Range(1, Double.PositiveInfinity)]
        public int? AdultFlag { get; set; }

        [Display(Name = "Item Shape")]
        [Range(1, Double.PositiveInfinity)]
        public int? ItemShape { get; set; }

        [Display(Name = "Occasion Description")]
        [Range(1, Double.PositiveInfinity)]
        public int? OccasionDescription { get; set; }

        [Display(Name = "Style Name")]
        [Range(1, Double.PositiveInfinity)]
        public int? StyleName { get; set; }

        [Display(Name = "Sleeve Type")]
        [Range(1, Double.PositiveInfinity)]
        public int? SleeveType { get; set; }

        [Display(Name = "Item Length")]
        [Range(1, Double.PositiveInfinity)]
        public int? ItemLength { get; set; }

        [Display(Name = "Bra Cup Size")]
        [Range(1, Double.PositiveInfinity)]
        public int? BraCupSize { get; set; }

        [Display(Name = "Bra Band Size")]
        [Range(1, Double.PositiveInfinity)]
        public int? BraBandSize { get; set; }

        [Display(Name = "Bra Band Size Unit")]
        [Range(1, Double.PositiveInfinity)]
        public int? BraBandSizeUnit { get; set; }

        [Display(Name = "Special Features")]
        [Range(1, Double.PositiveInfinity)]
        public int? SpecialFeatures { get; set; }

        [Display(Name = "Opacity/Transparency")]
        [Range(1, Double.PositiveInfinity)]
        public int? OpacityTransparency { get; set; }

        [Display(Name = "Closure Type")]
        [Range(1, Double.PositiveInfinity)]
        public int? ClosureType { get; set; }

        [Display(Name = "Bikini Top Style")]
        [Range(1, Double.PositiveInfinity)]
        public int? BikiniTopStyle { get; set; }

        [Display(Name = "Swimwear Bottom Style")]
        [Range(1, Double.PositiveInfinity)]
        public int? SwimwearBottomStyle { get; set; }

        [Display(Name = "Pattern Description")]
        [Range(1, Double.PositiveInfinity)]
        public int? PatternDescription { get; set; }

        [Display(Name = "Collar Style")]
        [Range(1, Double.PositiveInfinity)]
        public int? CollarStyle { get; set; }

        [Display(Name = "Fitting Type")]
        [Range(1, Double.PositiveInfinity)]
        public int? FittingType { get; set; }

        [Display(Name = "Neck Style")]
        [Range(1, Double.PositiveInfinity)]
        public int? NeckStyle { get; set; }

        [Display(Name = "Jeans Length Unit Of Measure")]
        [Range(1, Double.PositiveInfinity)]
        public int? JeansLengthUnitOfMeasure { get; set; }

        [Display(Name = "Jeans Length (Inches)")]
        [Range(1, Double.PositiveInfinity)]
        public int? JeansLengthInches { get; set; }

        [Display(Name = "Jeans Width Unit Of Measure")]
        [Range(1, Double.PositiveInfinity)]
        public int? JeansWidthUnitOfMeasure { get; set; }

        [Display(Name = "Jeans Width (Inches)")]
        [Range(1, Double.PositiveInfinity)]
        public int? JeansWidthInches { get; set; }
    }
}