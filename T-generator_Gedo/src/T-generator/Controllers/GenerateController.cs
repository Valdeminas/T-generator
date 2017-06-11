using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using T_generator.Data;
using Microsoft.AspNetCore.Identity;
using T_generator.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using T_generator.Models.Amazon.Data.Intermediate;
using T_generator.Models.Amazon.Data.Basic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.IO.Compression;

namespace T_generator.Controllers
{
    public class GenerateController : Controller
    {

        private readonly AmazonContext _context;
        private IHostingEnvironment _environment;

        public GenerateController(AmazonContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            //T_generator.Services.Amazon.EPPlusCore.EPPLusCore.test();
            AmazonAccount first = _context.AmazonAccounts.First();
            ViewData["AmazonAccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "Name",first.AmazonAccountID);
            ViewData["AmazonProductID"] = new MultiSelectList(_context.AmazonProducts.Where(i=>i.AmazonAccountID==first.AmazonAccountID), "AmazonProductID", "Name");
            return View();
        }

        public IActionResult GetProducts(int id)
        {          
            Dictionary<string,int> ProductList = new Dictionary<string, int>();
            foreach (AmazonProduct product in _context.AmazonProducts)
            {
                if (product.AmazonAccountID==id)
                {
                    ProductList[product.Name]=product.AmazonProductID;
                }

            }
            return Json(ProductList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Generate(int AmazonAccountID, List<int> Products)
        {
            AmazonAccount account = _context.AmazonAccounts.Single(i=>i.AmazonAccountID==AmazonAccountID);
            var marketplaces = _context.AccountMarketplaces.Where(i => i.AccountID == account.AmazonAccountID);
            var products = _context.AmazonProducts.Where(i => Products.Contains(i.AmazonProductID));

            var filenamesAndUrls = new Dictionary<string, string>();

            foreach (var marketplace in marketplaces)
            {
                var CurrentMarketplace = _context.AmazonMarketplaces.Where(i=>i.AmazonMarketplaceID==marketplace.MarketplaceID).SingleOrDefault();

                string templatePath = Path.Combine(_environment.WebRootPath, CurrentMarketplace.TemplateURL);

                

                int sheetNo = CurrentMarketplace.SheetNumber;
                int startingRow = CurrentMarketplace.StartingRow;
                int currentRow = startingRow;

                FileInfo newFile = new FileInfo(templatePath);

                using (ExcelPackage pck = new ExcelPackage(newFile))
                {                   
                    ExcelWorksheet worksheet = pck.Workbook.Worksheets.SingleOrDefault(i => i.Index==sheetNo);

                    var rows = worksheet.Dimension.Rows;
                    var columns = worksheet.Dimension.Columns;

                    foreach(var product in products)
                    {
                        var listings = _context.AmazonListings.Where(i => Products.Contains(i.AmazonProductID));

                        foreach (var listing in listings)
                        {
                            var productsizes = _context.ProductSizes.Where(i=>i.ProductID==product.AmazonProductID);
                            var currentDesign = _context.AmazonDesigns.Where(x => x.AmazonDesignID == listing.AmazonDesignID).First();
                            var currenType = _context.AmazonTypes.Where(x => x.AmazonTypeID == product.AmazonTypeID).First();

                            if (CurrentMarketplace.ItemSKU != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ItemSKU, account.Name + "-" + product.AmazonProductID);
                            }
                            if (CurrentMarketplace.ProductID != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductID, "");
                            }
                            if (CurrentMarketplace.ProductType != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductType, "");
                            }
                            if (CurrentMarketplace.ProductName != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductName, product.Name);
                            }
                            if (CurrentMarketplace.BrandName != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.BrandName, account.Name);
                            }
                            if (CurrentMarketplace.ClothingType != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ClothingType, product.AmazonType.Name);
                            }
                            if (CurrentMarketplace.ProductDescription != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductDescription, product.Description);
                            }
                            if (CurrentMarketplace.UpdateDelete != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.UpdateDelete, "");
                            }
                            if (CurrentMarketplace.ModelNumber != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ModelNumber, "");
                            }
                            if (CurrentMarketplace.ManufacturerPartNumber != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ManufacturerPartNumber, account.Name + "-" + product.AmazonProductID);
                            }
                            if (CurrentMarketplace.RelatedProductType != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.RelatedProductType, "");
                            }
                            if (CurrentMarketplace.RelatedProductID != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.RelatedProductID, "");
                            }
                            if (CurrentMarketplace.GtinExemptionReason != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.GtinExemptionReason, "");
                            }
                            if (CurrentMarketplace.StandardPrice != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.StandardPrice, "");
                            }
                            if (CurrentMarketplace.Quantity != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Quantity, "9999");
                            }
                            if (CurrentMarketplace.FulfillmentLatency != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.FulfillmentLatency, "");
                            }
                            if (CurrentMarketplace.SalePrice != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.SalePrice, "");
                            }
                            if (CurrentMarketplace.SaleFromDate != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.SaleFromDate, "");
                            }
                            if (CurrentMarketplace.SaleEndDate != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.SaleEndDate, "");
                            }
                            if (CurrentMarketplace.MaxAggrShipQuant != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.MaxAggrShipQuant, "");
                            }
                            if (CurrentMarketplace.PackageQuantity != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageQuantity, "");
                            }
                            if (CurrentMarketplace.NumberOfItems != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.NumberOfItems, "");
                            }
                            if (CurrentMarketplace.CanBeGiftMessaged != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.CanBeGiftMessaged, "");
                            }
                            if (CurrentMarketplace.CanBeGiftWrapped != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.CanBeGiftWrapped, "");
                            }
                            if (CurrentMarketplace.IsDiscontinued != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.IsDiscontinued, "");
                            }
                            if (CurrentMarketplace.LaunchDate != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.LaunchDate, "");
                            }
                            if (CurrentMarketplace.MerchantShippingGroup != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.MerchantShippingGroup, "");
                            }
                            if (CurrentMarketplace.ShippingWeight != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ShippingWeight, "");
                            }
                            if (CurrentMarketplace.WeightUnitOfMeasure != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.WeightUnitOfMeasure, "");
                            }
                            if (CurrentMarketplace.BrowseNode != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.BrowseNode, "");
                            }
                            if (CurrentMarketplace.SearchTerms != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.SearchTerms, product.Description);
                            }
                            if (CurrentMarketplace.Features1 != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features1, product.Keywords==null ? "" : product.Keywords.ToList()[0].Keyword.Keyword);
                            }
                            if (CurrentMarketplace.Features2 != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features2, product.Keywords == null ? "" : product.Keywords.ToList()[1].Keyword.Keyword);
                            }
                            if (CurrentMarketplace.Features3 != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features3, product.Keywords == null ? "" : product.Keywords.ToList()[2].Keyword.Keyword);
                            }
                            if (CurrentMarketplace.Features4 != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features4, product.Keywords == null ? "" : product.Keywords.ToList()[3].Keyword.Keyword);
                            }
                            if (CurrentMarketplace.Features5 != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features5, product.Keywords == null ? "" : product.Keywords.ToList()[4].Keyword.Keyword);
                            }
                            if (CurrentMarketplace.MainImgUrl != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.MainImgUrl, Path.Combine(Request.Host.Value, listing.DesignURL));
                            }
                            if (CurrentMarketplace.OtherImgUrl != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.OtherImgUrl, "");
                            }
                            if (CurrentMarketplace.SwatchImgUrl != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.SwatchImgUrl, "");
                            }
                            if (CurrentMarketplace.FulfillmentCentreId != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.FulfillmentCentreId, "");
                            }
                            if (CurrentMarketplace.PackageLength != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageLength, "");
                            }
                            if (CurrentMarketplace.PackageWidth != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageWidth, "");
                            }
                            if (CurrentMarketplace.PackageHeight != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageHeight, "");
                            }
                            if (CurrentMarketplace.PackageLengthUnitOfMeasure != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageLengthUnitOfMeasure, "");
                            }
                            if (CurrentMarketplace.PackageWeight != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageWeight, "");
                            }
                            if (CurrentMarketplace.PackageWeightUnitOfMeasure != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageWeightUnitOfMeasure, "");
                            }
                            if (CurrentMarketplace.PackageDimensionsUnitOfMeasure != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageDimensionsUnitOfMeasure, "");
                            }
                            if (CurrentMarketplace.Parentage != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Parentage, "parent");
                            }
                            if (CurrentMarketplace.ParentSKU != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ParentSKU, "");
                            }
                            if (CurrentMarketplace.VariationTheme != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.VariationTheme, "size-color");
                            }
                            if (CurrentMarketplace.CountryOfOrigin != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.CountryOfOrigin, "Lithuania");
                            }
                            if (CurrentMarketplace.ColourMap != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ColourMap, "");
                            }
                            if (CurrentMarketplace.Colour != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Colour, "");
                            }
                            if (CurrentMarketplace.SizeMap != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Colour, "");
                            }
                            if (CurrentMarketplace.Size != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Size, "");
                            }
                            if (CurrentMarketplace.MaterialComposition != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.MaterialComposition, "");
                            }
                            if (CurrentMarketplace.OuterMaterialType != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.OuterMaterialType, "");
                            }
                            if (CurrentMarketplace.InnerMaterialType != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.InnerMaterialType, "");
                            }
                            if (CurrentMarketplace.SeasonAndCollectionYear != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.SeasonAndCollectionYear, "");
                            }
                            if (CurrentMarketplace.ProductCareInstructions != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductCareInstructions, "");
                            }
                            if (CurrentMarketplace.ModelName != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ModelName, "");
                            }
                            if (CurrentMarketplace.Department != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.Department, "");
                            }
                            if (CurrentMarketplace.AdultFlag != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.AdultFlag, "");
                            }
                            if (CurrentMarketplace.ItemShape != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ItemShape, "");
                            }
                            if (CurrentMarketplace.OccasionDescription != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.OccasionDescription, "");
                            }
                            if (CurrentMarketplace.StyleName != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.StyleName, "");
                            }
                            if (CurrentMarketplace.SleeveType != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.SleeveType, "");
                            }
                            if (CurrentMarketplace.ItemLength != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ItemLength, "");
                            }
                            if (CurrentMarketplace.BraCupSize != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.BraCupSize, "");
                            }
                            if (CurrentMarketplace.BraBandSize != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.BraBandSize, "");
                            }
                            if (CurrentMarketplace.BraBandSizeUnit != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.BraBandSizeUnit, "");
                            }
                            if (CurrentMarketplace.SpecialFeatures != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.SpecialFeatures, "");
                            }
                            if (CurrentMarketplace.OpacityTransparency != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.OpacityTransparency, "");
                            }
                            if (CurrentMarketplace.ClosureType != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.ClosureType, "");
                            }
                            if (CurrentMarketplace.BikiniTopStyle != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.BikiniTopStyle, "");
                            }
                            if (CurrentMarketplace.SwimwearBottomStyle != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.SwimwearBottomStyle, "");
                            }
                            if (CurrentMarketplace.PatternDescription != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.PatternDescription, "");
                            }
                            if (CurrentMarketplace.CollarStyle != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.CollarStyle, "");
                            }
                            if (CurrentMarketplace.FittingType != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.FittingType, "");
                            }
                            if (CurrentMarketplace.NeckStyle != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.NeckStyle, "");
                            }
                            if (CurrentMarketplace.JeansLengthUnitOfMeasure != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.JeansLengthUnitOfMeasure, "");
                            }
                            if (CurrentMarketplace.JeansLengthInches != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.JeansLengthInches, "");
                            }
                            if (CurrentMarketplace.JeansWidthUnitOfMeasure != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.JeansWidthUnitOfMeasure, "");
                            }
                            if (CurrentMarketplace.JeansWidthInches != null)
                            {
                                worksheet.SetValue(currentRow, (int)CurrentMarketplace.JeansWidthInches, "");
                            }

                            currentRow++;

                            foreach (var productsize in productsizes)
                            {
                                var size = _context.AmazonSizes.Where(i => i.AmazonSizeID == productsize.SizeID).First();

                                if (CurrentMarketplace.ItemSKU != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ItemSKU, account.Name + "-" + product.AmazonProductID + "-" + size.Name);
                                }
                                if (CurrentMarketplace.ProductID != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductID, "");
                                }
                                if (CurrentMarketplace.ProductType != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductType, "");
                                }
                                if (CurrentMarketplace.ProductName != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductName, product.Name);
                                }
                                if (CurrentMarketplace.BrandName != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.BrandName, account.Name);
                                }
                                if (CurrentMarketplace.ClothingType != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ClothingType, product.AmazonType.Name);
                                }
                                if (CurrentMarketplace.ProductDescription != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductDescription, product.Description);
                                }
                                if (CurrentMarketplace.UpdateDelete != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.UpdateDelete, "");
                                }
                                if (CurrentMarketplace.ModelNumber != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ModelNumber, "");
                                }
                                if (CurrentMarketplace.ManufacturerPartNumber != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ManufacturerPartNumber, account.Name + "-" + product.AmazonProductID + "-" + size.Name);
                                }
                                if (CurrentMarketplace.RelatedProductType != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.RelatedProductType, "");
                                }
                                if (CurrentMarketplace.RelatedProductID != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.RelatedProductID, "");
                                }
                                if (CurrentMarketplace.GtinExemptionReason != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.GtinExemptionReason, "");
                                }
                                if (CurrentMarketplace.StandardPrice != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.StandardPrice, "");
                                }
                                if (CurrentMarketplace.Quantity != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Quantity, "9999");
                                }
                                if (CurrentMarketplace.FulfillmentLatency != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.FulfillmentLatency, "");
                                }
                                if (CurrentMarketplace.SalePrice != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.SalePrice, "");
                                }
                                if (CurrentMarketplace.SaleFromDate != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.SaleFromDate, "");
                                }
                                if (CurrentMarketplace.SaleEndDate != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.SaleEndDate, "");
                                }
                                if (CurrentMarketplace.MaxAggrShipQuant != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.MaxAggrShipQuant, "");
                                }
                                if (CurrentMarketplace.PackageQuantity != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageQuantity, "");
                                }
                                if (CurrentMarketplace.NumberOfItems != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.NumberOfItems, "");
                                }
                                if (CurrentMarketplace.CanBeGiftMessaged != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.CanBeGiftMessaged, "");
                                }
                                if (CurrentMarketplace.CanBeGiftWrapped != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.CanBeGiftWrapped, "");
                                }
                                if (CurrentMarketplace.IsDiscontinued != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.IsDiscontinued, "");
                                }
                                if (CurrentMarketplace.LaunchDate != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.LaunchDate, "");
                                }
                                if (CurrentMarketplace.MerchantShippingGroup != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.MerchantShippingGroup, "");
                                }
                                if (CurrentMarketplace.ShippingWeight != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ShippingWeight, "");
                                }
                                if (CurrentMarketplace.WeightUnitOfMeasure != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.WeightUnitOfMeasure, "");
                                }
                                if (CurrentMarketplace.BrowseNode != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.BrowseNode, "");
                                }
                                if (CurrentMarketplace.SearchTerms != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.SearchTerms, product.Description);
                                }
                                if (CurrentMarketplace.Features1 != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features1, product.Keywords == null ? "" : product.Keywords.ToList()[0].Keyword.Keyword);
                                }
                                if (CurrentMarketplace.Features2 != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features2, product.Keywords == null ? "" : product.Keywords.ToList()[1].Keyword.Keyword);
                                }
                                if (CurrentMarketplace.Features3 != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features3, product.Keywords == null ? "" : product.Keywords.ToList()[2].Keyword.Keyword);
                                }
                                if (CurrentMarketplace.Features4 != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features4, product.Keywords == null ? "" : product.Keywords.ToList()[3].Keyword.Keyword);
                                }
                                if (CurrentMarketplace.Features5 != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Features5, product.Keywords == null ? "" : product.Keywords.ToList()[4].Keyword.Keyword);
                                }
                                if (CurrentMarketplace.MainImgUrl != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.MainImgUrl, Path.Combine(Request.Host.Value, listing.DesignURL));
                                }
                                if (CurrentMarketplace.OtherImgUrl != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.OtherImgUrl, "");
                                }
                                if (CurrentMarketplace.SwatchImgUrl != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.SwatchImgUrl, "");
                                }
                                if (CurrentMarketplace.FulfillmentCentreId != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.FulfillmentCentreId, "");
                                }
                                if (CurrentMarketplace.PackageLength != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageLength, "");
                                }
                                if (CurrentMarketplace.PackageWidth != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageWidth, "");
                                }
                                if (CurrentMarketplace.PackageHeight != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageHeight, "");
                                }
                                if (CurrentMarketplace.PackageLengthUnitOfMeasure != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageLengthUnitOfMeasure, "");
                                }
                                if (CurrentMarketplace.PackageWeight != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageWeight, "");
                                }
                                if (CurrentMarketplace.PackageWeightUnitOfMeasure != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageWeightUnitOfMeasure, "");
                                }
                                if (CurrentMarketplace.PackageDimensionsUnitOfMeasure != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.PackageDimensionsUnitOfMeasure, "");
                                }
                                if (CurrentMarketplace.Parentage != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Parentage, "child");
                                }
                                if (CurrentMarketplace.ParentSKU != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ParentSKU, account.Name + " - " + product.AmazonProductID);
                                }
                                if (CurrentMarketplace.VariationTheme != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.VariationTheme, "size-color");
                                }
                                if (CurrentMarketplace.CountryOfOrigin != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.CountryOfOrigin, "Lithuania");
                                }
                                if (CurrentMarketplace.ColourMap != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ColourMap, "");
                                }
                                if (CurrentMarketplace.Colour != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Colour, "");
                                }
                                if (CurrentMarketplace.SizeMap != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Colour, "");
                                }
                                if (CurrentMarketplace.Size != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Size, "");
                                }
                                if (CurrentMarketplace.MaterialComposition != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.MaterialComposition, "");
                                }
                                if (CurrentMarketplace.OuterMaterialType != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.OuterMaterialType, "");
                                }
                                if (CurrentMarketplace.InnerMaterialType != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.InnerMaterialType, "");
                                }
                                if (CurrentMarketplace.SeasonAndCollectionYear != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.SeasonAndCollectionYear, "");
                                }
                                if (CurrentMarketplace.ProductCareInstructions != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ProductCareInstructions, "");
                                }
                                if (CurrentMarketplace.ModelName != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ModelName, "");
                                }
                                if (CurrentMarketplace.Department != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.Department, "");
                                }
                                if (CurrentMarketplace.AdultFlag != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.AdultFlag, "");
                                }
                                if (CurrentMarketplace.ItemShape != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ItemShape, "");
                                }
                                if (CurrentMarketplace.OccasionDescription != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.OccasionDescription, "");
                                }
                                if (CurrentMarketplace.StyleName != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.StyleName, "");
                                }
                                if (CurrentMarketplace.SleeveType != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.SleeveType, "");
                                }
                                if (CurrentMarketplace.ItemLength != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ItemLength, "");
                                }
                                if (CurrentMarketplace.BraCupSize != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.BraCupSize, "");
                                }
                                if (CurrentMarketplace.BraBandSize != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.BraBandSize, "");
                                }
                                if (CurrentMarketplace.BraBandSizeUnit != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.BraBandSizeUnit, "");
                                }
                                if (CurrentMarketplace.SpecialFeatures != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.SpecialFeatures, "");
                                }
                                if (CurrentMarketplace.OpacityTransparency != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.OpacityTransparency, "");
                                }
                                if (CurrentMarketplace.ClosureType != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.ClosureType, "");
                                }
                                if (CurrentMarketplace.BikiniTopStyle != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.BikiniTopStyle, "");
                                }
                                if (CurrentMarketplace.SwimwearBottomStyle != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.SwimwearBottomStyle, "");
                                }
                                if (CurrentMarketplace.PatternDescription != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.PatternDescription, "");
                                }
                                if (CurrentMarketplace.CollarStyle != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.CollarStyle, "");
                                }
                                if (CurrentMarketplace.FittingType != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.FittingType, "");
                                }
                                if (CurrentMarketplace.NeckStyle != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.NeckStyle, "");
                                }
                                if (CurrentMarketplace.JeansLengthUnitOfMeasure != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.JeansLengthUnitOfMeasure, "");
                                }
                                if (CurrentMarketplace.JeansLengthInches != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.JeansLengthInches, "");
                                }
                                if (CurrentMarketplace.JeansWidthUnitOfMeasure != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.JeansWidthUnitOfMeasure, "");
                                }
                                if (CurrentMarketplace.JeansWidthInches != null)
                                {
                                    worksheet.SetValue(currentRow, (int)CurrentMarketplace.JeansWidthInches, "");
                                }

                                currentRow++;
                            }
                        }

                    }

                    string generatedPath = templatePath.Replace("Templates", "Temp");
                    FileInfo generatedFile = new FileInfo(generatedPath);
                    pck.SaveAs(generatedFile);
                    filenamesAndUrls[CurrentMarketplace.Name] = generatedPath;

                    //pck.Save();
                    //filenamesAndUrls[CurrentMarketplace.Name] = templatePath;

                }

            }
            var memoryStream = new MemoryStream();
            ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

            // loop over a series of Azure blobs which contain text
            foreach (var kvp in filenamesAndUrls){

                var fileStream = new FileStream(kvp.Value,FileMode.Open);
                var zipEntry = archive.CreateEntry(kvp.Key + ".xlsx");

                using (Stream zipStream = zipEntry.Open())
                {
                    fileStream.CopyTo(zipStream);
                }
                fileStream.Dispose();
                
            }

            archive.Dispose();
            memoryStream.Seek(0, SeekOrigin.Begin);

            FileStreamResult result = new FileStreamResult(memoryStream, "application/zip");
            result.FileDownloadName = "Generated.zip";

            // loop over a series of Azure blobs which contain text
            foreach (var kvp in filenamesAndUrls)
            {
                System.IO.File.Delete(kvp.Value);
            }

            return result;
        }
    }

    
}
