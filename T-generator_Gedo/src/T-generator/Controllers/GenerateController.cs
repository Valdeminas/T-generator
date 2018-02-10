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
//using OfficeOpenXml;
using NPOI;
using System.IO.Compression;
using T_generator.Models.Amazon.Data.JoinTables;
using NPOI.SS.UserModel;

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
            ViewData["AmazonAccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "Name", first.AmazonAccountID);
            ViewData["AmazonProductID"] = new MultiSelectList(_context.AmazonProducts.Where(i => i.AmazonAccountID == first.AmazonAccountID), "AmazonProductID", "Name");
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
        public IActionResult Index(int AmazonAccountID, List<int> Products)
        {
            AmazonAccount account = _context.AmazonAccounts.Single(i=>i.AmazonAccountID==AmazonAccountID);
            var marketplaces = _context.AccountMarketplaces.Where(i => i.AccountID == account.AmazonAccountID);
            var products = _context.AmazonProducts.Where(i => Products.Contains(i.AmazonProductID));

            var filenamesAndUrls = new Dictionary<string, string>();

            foreach (var marketplace in marketplaces)
            {
                var CurrentMarketplace = _context.AmazonMarketplaces.Where(i=>i.AmazonMarketplaceID==marketplace.MarketplaceID).SingleOrDefault();
                var CurrentTemplates = _context.AmazonTemplates.Where(i => i.AmazonMarketplaceID == marketplace.MarketplaceID).ToArray();

                if (CurrentTemplates.Length == 0)
                {
                    AmazonAccount first = _context.AmazonAccounts.First();
                    ViewData["AmazonAccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "Name", first.AmazonAccountID);
                    ViewData["AmazonProductID"] = new MultiSelectList(_context.AmazonProducts.Where(i => i.AmazonAccountID == first.AmazonAccountID), "AmazonProductID", "Name");
                    ViewData["Error"] = "There are no templates for marketplace " + CurrentMarketplace.Name + ".";
                    return View();
                }

                foreach (var product in products)
                {

                    AmazonTemplate currentTemplate=null;

                    foreach(AmazonTemplate template in CurrentTemplates)
                    {
                        var type=_context.TemplateTypes.Where(i => i.TemplateID == template.AmazonTemplateID).Where(i=>i.TypeID==product.AmazonTypeID).FirstOrDefault();
                        if (type != null)
                        {
                            currentTemplate = template;
                            break;
                        }                       
                    }

                    if (currentTemplate == null)
                    {
                        AmazonAccount first = _context.AmazonAccounts.First();
                        ViewData["AmazonAccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "Name", first.AmazonAccountID);
                        ViewData["AmazonProductID"] = new MultiSelectList(_context.AmazonProducts.Where(i => i.AmazonAccountID == first.AmazonAccountID), "AmazonProductID", "Name");
                        var AmazonType = _context.AmazonTypes.Where(i => i.AmazonTypeID == product.AmazonTypeID).SingleOrDefault();
                        ViewData["Error"] = "There is no template of type " + AmazonType.Name + " , for product " + product.Name + ".";
                        return View();
                    }

                string templatePath = Path.Combine(_environment.WebRootPath, currentTemplate.TemplateURL);
               
                int sheetNo = currentTemplate.SheetNumber;
                int startingRow = currentTemplate.StartingRow;
                int currentRow = startingRow-1;

                    IWorkbook workbook = null;      
                    ISheet worksheet = null;

                    FileInfo newFile = new FileInfo(templatePath);

                    //using (ExcelPackage pck = new ExcelPackage(newFile))
                    using (FileStream FS=new FileStream(templatePath,FileMode.Open,FileAccess.ReadWrite))
                    {
                        workbook = WorkbookFactory.Create(FS);
                        worksheet = workbook.GetSheetAt(sheetNo-1);
                        //ExcelWorksheet worksheet = pck.Workbook.Worksheets.SingleOrDefault(i => i.Index == sheetNo);

                        //var rows = worksheet.Dimension.Rows;
                       // var columns = worksheet.Dimension.Columns;

                        var rows = worksheet.LastRowNum;
                        //var columns = worksheet.col;


                        var listings = _context.AmazonListings.Where(i => Products.Contains(i.AmazonProductID));

                        foreach (var listing in listings)
                        {
                            var row = worksheet.GetRow(currentRow);
                            var cell=row.GetCell(0);
                            var productsizes = _context.ProductSizes.Where(i => i.ProductID == product.AmazonProductID);
                            var currentDesign = _context.AmazonDesigns.Where(x => x.AmazonDesignID == listing.AmazonDesignID).First();
                            var currenType = _context.AmazonTypes.Where(x => x.AmazonTypeID == product.AmazonTypeID).First();
                            var designMarketplaces = _context.DesignMarketplaces.Where(a => a.DesignID == currentDesign.AmazonDesignID).Where(a=>a.MarketplaceID==CurrentMarketplace.AmazonMarketplaceID);

                            //if (!designMarketplaces.Contains(new DesignMarketplaces { MarketplaceID = CurrentMarketplace.AmazonMarketplaceID, DesignID = currentDesign.AmazonDesignID }))
                            //{
                            //    continue;
                            //}

                            if (designMarketplaces.Count() == 0)
                            {
                                continue;
                            }

                            if (currentTemplate.ItemSKU != null)
                            {
                                cell=row.CreateCell((int)currentTemplate.ItemSKU);
                                cell.SetCellValue(account.Name + "-" + product.AmazonProductID);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ItemSKU, account.Name + "-" + product.AmazonProductID);
                            }
                            if (currentTemplate.ProductID != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ProductID);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ProductID, "");
                            }
                            if (currentTemplate.ProductType != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ProductType);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ProductType, "");
                            }
                            if (currentTemplate.ProductName != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ProductName);
                                cell.SetCellValue(product.Name);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ProductName, product.Name);
                            }
                            if (currentTemplate.BrandName != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.BrandName);
                                cell.SetCellValue(account.Name);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.BrandName, account.Name);
                            }
                            if (currentTemplate.ClothingType != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ClothingType);
                                cell.SetCellValue(product.AmazonType.Name);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ClothingType, product.AmazonType.Name);
                            }
                            if (currentTemplate.ProductDescription != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ProductDescription);
                                cell.SetCellValue(product.Description);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ProductDescription, product.Description);
                            }
                            if (currentTemplate.UpdateDelete != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.UpdateDelete);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.UpdateDelete, "");
                            }
                            if (currentTemplate.ModelNumber != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ModelNumber);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ModelNumber, "");
                            }
                            if (currentTemplate.ManufacturerPartNumber != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ManufacturerPartNumber);
                                cell.SetCellValue(account.Name + "-" + product.AmazonProductID);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ManufacturerPartNumber, account.Name + "-" + product.AmazonProductID);
                            }
                            if (currentTemplate.RelatedProductType != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.RelatedProductType);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.RelatedProductType, "");
                            }
                            if (currentTemplate.RelatedProductID != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.RelatedProductID);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.RelatedProductID, "");
                            }
                            if (currentTemplate.GtinExemptionReason != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.GtinExemptionReason);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.GtinExemptionReason, "");
                            }
                            if (currentTemplate.StandardPrice != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.StandardPrice);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.StandardPrice, "");
                            }
                            if (currentTemplate.Quantity != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Quantity);
                                cell.SetCellValue("9999");
                               // worksheet.SetValue(currentRow, (int)currentTemplate.Quantity, "9999");
                            }
                            if (currentTemplate.FulfillmentLatency != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.FulfillmentLatency);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.FulfillmentLatency, "");
                            }
                            if (currentTemplate.SalePrice != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SalePrice);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.SalePrice, "");
                            }
                            if (currentTemplate.SaleFromDate != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SaleFromDate);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.SaleFromDate, "");
                            }
                            if (currentTemplate.SaleEndDate != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SaleEndDate);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.SaleEndDate, "");
                            }
                            if (currentTemplate.MaxAggrShipQuant != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.MaxAggrShipQuant);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.MaxAggrShipQuant, "");
                            }
                            if (currentTemplate.PackageQuantity != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.PackageQuantity);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.PackageQuantity, "");
                            }
                            if (currentTemplate.NumberOfItems != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.NumberOfItems);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.NumberOfItems, "");
                            }
                            if (currentTemplate.CanBeGiftMessaged != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.CanBeGiftMessaged);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.CanBeGiftMessaged, "");
                            }
                            if (currentTemplate.CanBeGiftWrapped != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.CanBeGiftWrapped);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.CanBeGiftWrapped, "");
                            }
                            if (currentTemplate.IsDiscontinued != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.IsDiscontinued);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.IsDiscontinued, "");
                            }
                            if (currentTemplate.LaunchDate != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.LaunchDate);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.LaunchDate, "");
                            }
                            if (currentTemplate.MerchantShippingGroup != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.MerchantShippingGroup);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.MerchantShippingGroup, "");
                            }
                            if (currentTemplate.ShippingWeight != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ShippingWeight);
                                cell.SetCellValue("");
                               // worksheet.SetValue(currentRow, (int)currentTemplate.ShippingWeight, "");
                            }
                            if (currentTemplate.WeightUnitOfMeasure != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.WeightUnitOfMeasure);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.WeightUnitOfMeasure, "");
                            }
                            if (currentTemplate.BrowseNode != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.BrowseNode);
                                cell.SetCellValue("");
                               // worksheet.SetValue(currentRow, (int)currentTemplate.BrowseNode, "");
                            }
                            if (currentTemplate.SearchTerms != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SearchTerms);
                                cell.SetCellValue(product.Description);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.SearchTerms, product.Description);
                            }
                            if (currentTemplate.Features1 != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Features1);
                                cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[0].Keyword.Keyword);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Features1, product.Keywords == null ? "" : product.Keywords.ToList()[0].Keyword.Keyword);
                            }
                            if (currentTemplate.Features2 != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Features2);
                                cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[1].Keyword.Keyword);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Features2, product.Keywords == null ? "" : product.Keywords.ToList()[1].Keyword.Keyword);
                            }
                            if (currentTemplate.Features3 != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Features3);
                                cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[2].Keyword.Keyword);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Features3, product.Keywords == null ? "" : product.Keywords.ToList()[2].Keyword.Keyword);
                            }
                            if (currentTemplate.Features4 != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Features4);
                                cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[3].Keyword.Keyword);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Features4, product.Keywords == null ? "" : product.Keywords.ToList()[3].Keyword.Keyword);
                            }
                            if (currentTemplate.Features5 != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Features5);
                                cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[4].Keyword.Keyword);
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Features5, product.Keywords == null ? "" : product.Keywords.ToList()[4].Keyword.Keyword);
                            }
                            if (currentTemplate.MainImgUrl != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.MainImgUrl);
                                cell.SetCellValue(Path.Combine(Request.Host.Value, listing.DesignURL));
                                //worksheet.SetValue(currentRow, (int)currentTemplate.MainImgUrl, Path.Combine(Request.Host.Value, listing.DesignURL));
                            }
                            if (currentTemplate.OtherImgUrl != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.OtherImgUrl);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.OtherImgUrl, "");
                            }
                            if (currentTemplate.OtherImgUrl2 != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.OtherImgUrl2);
                                cell.SetCellValue("");
                               // worksheet.SetValue(currentRow, (int)currentTemplate.OtherImgUrl2, "");
                            }
                            if (currentTemplate.OtherImgUrl3 != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.OtherImgUrl3);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.OtherImgUrl3, "");
                            }
                            if (currentTemplate.SwatchImgUrl != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SwatchImgUrl);
                                cell.SetCellValue("");
                               // worksheet.SetValue(currentRow, (int)currentTemplate.SwatchImgUrl, "");
                            }
                            if (currentTemplate.FulfillmentCentreId != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.FulfillmentCentreId);
                                cell.SetCellValue("");
                               // worksheet.SetValue(currentRow, (int)currentTemplate.FulfillmentCentreId, "");
                            }
                            if (currentTemplate.PackageLength != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.PackageLength);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.PackageLength, "");
                            }
                            if (currentTemplate.PackageWidth != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.PackageWidth);
                                cell.SetCellValue("");
                               // worksheet.SetValue(currentRow, (int)currentTemplate.PackageWidth, "");
                            }
                            if (currentTemplate.PackageHeight != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.PackageHeight);
                                cell.SetCellValue("");
                               // worksheet.SetValue(currentRow, (int)currentTemplate.PackageHeight, "");
                            }
                            if (currentTemplate.PackageLengthUnitOfMeasure != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.PackageLengthUnitOfMeasure);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.PackageLengthUnitOfMeasure, "");
                            }
                            if (currentTemplate.PackageWeight != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.PackageWeight);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.PackageWeight, "");
                            }
                            if (currentTemplate.PackageWeightUnitOfMeasure != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.PackageWeightUnitOfMeasure);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.PackageWeightUnitOfMeasure, "");
                            }
                            if (currentTemplate.PackageDimensionsUnitOfMeasure != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.PackageDimensionsUnitOfMeasure);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.PackageDimensionsUnitOfMeasure, "");
                            }
                            if (currentTemplate.Parentage != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Parentage);
                                cell.SetCellValue("parent");                          
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Parentage, "parent");
                            }
                            if (currentTemplate.ParentSKU != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ParentSKU);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ParentSKU, "");
                            }
                            if (currentTemplate.VariationTheme != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.VariationTheme);
                                cell.SetCellValue("size-color");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.VariationTheme, "size-color");
                            }
                            if (currentTemplate.CountryOfOrigin != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.CountryOfOrigin);
                                cell.SetCellValue("Lithuania");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.CountryOfOrigin, "Lithuania");
                            }
                            if (currentTemplate.ColourMap != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ColourMap);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ColourMap, "");
                            }
                            if (currentTemplate.Colour != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Colour);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Colour, "");
                            }
                            if (currentTemplate.SizeMap != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SizeMap);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Colour, "");
                            }
                            if (currentTemplate.Size != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Size);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Size, "");
                            }
                            if (currentTemplate.MaterialComposition != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.MaterialComposition);
                                cell.SetCellValue("");
                               // worksheet.SetValue(currentRow, (int)currentTemplate.MaterialComposition, "");
                            }
                            if (currentTemplate.OuterMaterialType != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.OuterMaterialType);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.OuterMaterialType, "");
                            }
                            if (currentTemplate.InnerMaterialType != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.InnerMaterialType);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.InnerMaterialType, "");
                            }
                            if (currentTemplate.SeasonAndCollectionYear != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SeasonAndCollectionYear);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.SeasonAndCollectionYear, "");
                            }
                            if (currentTemplate.ProductCareInstructions != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ProductCareInstructions);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ProductCareInstructions, "");
                            }
                            if (currentTemplate.ModelName != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ModelName);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ModelName, "");
                            }
                            if (currentTemplate.Department != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.Department);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.Department, "");
                            }
                            if (currentTemplate.AdultFlag != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.AdultFlag);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.AdultFlag, "");
                            }
                            if (currentTemplate.ItemShape != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ItemShape);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ItemShape, "");
                            }
                            if (currentTemplate.OccasionDescription != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.OccasionDescription);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.OccasionDescription, "");
                            }
                            if (currentTemplate.StyleName != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.StyleName);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.StyleName, "");
                            }
                            if (currentTemplate.SleeveType != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SleeveType);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.SleeveType, "");
                            }
                            if (currentTemplate.ItemLength != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ItemLength);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ItemLength, "");
                            }
                            if (currentTemplate.BraCupSize != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.BraCupSize);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.BraCupSize, "");
                            }
                            if (currentTemplate.BraBandSize != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.BraBandSize);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.BraBandSize, "");
                            }
                            if (currentTemplate.BraBandSizeUnit != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.BraBandSizeUnit);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.BraBandSizeUnit, "");
                            }
                            if (currentTemplate.SpecialFeatures != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SpecialFeatures);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.SpecialFeatures, "");
                            }
                            if (currentTemplate.OpacityTransparency != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.OpacityTransparency);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.OpacityTransparency, "");
                            }
                            if (currentTemplate.ClosureType != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.ClosureType);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.ClosureType, "");
                            }
                            if (currentTemplate.BikiniTopStyle != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.BikiniTopStyle);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.BikiniTopStyle, "");
                            }
                            if (currentTemplate.SwimwearBottomStyle != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.SwimwearBottomStyle);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.SwimwearBottomStyle, "");
                            }
                            if (currentTemplate.PatternDescription != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.PatternDescription);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.PatternDescription, "");
                            }
                            if (currentTemplate.CollarStyle != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.CollarStyle);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.CollarStyle, "");
                            }
                            if (currentTemplate.FittingType != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.FittingType);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.FittingType, "");
                            }
                            if (currentTemplate.NeckStyle != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.NeckStyle);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.NeckStyle, "");
                            }
                            if (currentTemplate.JeansLengthUnitOfMeasure != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.JeansLengthUnitOfMeasure);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.JeansLengthUnitOfMeasure, "");
                            }
                            if (currentTemplate.JeansLengthInches != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.JeansLengthInches);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.JeansLengthInches, "");
                            }
                            if (currentTemplate.JeansWidthUnitOfMeasure != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.JeansWidthUnitOfMeasure);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.JeansWidthUnitOfMeasure, "");
                            }
                            if (currentTemplate.JeansWidthInches != null)
                            {
                                cell = row.CreateCell((int)currentTemplate.JeansWidthInches);
                                cell.SetCellValue("");
                                //worksheet.SetValue(currentRow, (int)currentTemplate.JeansWidthInches, "");
                            }

                            currentRow++;

                            foreach (var productsize in productsizes)
                            {
                                var size = _context.AmazonSizes.Where(i => i.AmazonSizeID == productsize.SizeID).First();
                                row = worksheet.GetRow(currentRow);
                                if (currentTemplate.ItemSKU != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ItemSKU);
                                    cell.SetCellValue(account.Name + "-" + product.AmazonProductID + "-" + size.Name);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ItemSKU, account.Name + "-" + product.AmazonProductID + "-" + size.Name);
                                }
                                if (currentTemplate.ProductID != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ProductID);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ProductID, "");
                                }
                                if (currentTemplate.ProductType != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ProductType);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ProductType, "");
                                }
                                if (currentTemplate.ProductName != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ProductName);
                                    cell.SetCellValue(product.Name);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ProductName, product.Name);
                                }
                                if (currentTemplate.BrandName != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.BrandName);
                                    cell.SetCellValue(account.Name);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.BrandName, account.Name);
                                }
                                if (currentTemplate.ClothingType != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ClothingType);
                                    cell.SetCellValue(product.AmazonType.Name);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ClothingType, product.AmazonType.Name);
                                }
                                if (currentTemplate.ProductDescription != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ProductDescription);
                                    cell.SetCellValue(product.Description);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ProductDescription, product.Description);
                                }
                                if (currentTemplate.UpdateDelete != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.UpdateDelete);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.UpdateDelete, "");
                                }
                                if (currentTemplate.ModelNumber != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ModelNumber);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ModelNumber, "");
                                }
                                if (currentTemplate.ManufacturerPartNumber != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ManufacturerPartNumber);
                                    cell.SetCellValue(account.Name + "-" + product.AmazonProductID + "-" + size.Name);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ManufacturerPartNumber, account.Name + "-" + product.AmazonProductID + "-" + size.Name);
                                }
                                if (currentTemplate.RelatedProductType != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.RelatedProductType);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.RelatedProductType, "");
                                }
                                if (currentTemplate.RelatedProductID != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.RelatedProductID);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.RelatedProductID, "");
                                }
                                if (currentTemplate.GtinExemptionReason != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.GtinExemptionReason);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.GtinExemptionReason, "");
                                }
                                if (currentTemplate.StandardPrice != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.StandardPrice);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.StandardPrice, "");
                                }
                                if (currentTemplate.Quantity != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Quantity);
                                    cell.SetCellValue("9999");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Quantity, "9999");
                                }
                                if (currentTemplate.FulfillmentLatency != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.FulfillmentLatency);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.FulfillmentLatency, "");
                                }
                                if (currentTemplate.SalePrice != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SalePrice);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.SalePrice, "");
                                }
                                if (currentTemplate.SaleFromDate != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SaleFromDate);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.SaleFromDate, "");
                                }
                                if (currentTemplate.SaleEndDate != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SaleEndDate);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.SaleEndDate, "");
                                }
                                if (currentTemplate.MaxAggrShipQuant != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.MaxAggrShipQuant);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.MaxAggrShipQuant, "");
                                }
                                if (currentTemplate.PackageQuantity != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.PackageQuantity);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.PackageQuantity, "");
                                }
                                if (currentTemplate.NumberOfItems != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.NumberOfItems);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.NumberOfItems, "");
                                }
                                if (currentTemplate.CanBeGiftMessaged != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.CanBeGiftMessaged);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.CanBeGiftMessaged, "");
                                }
                                if (currentTemplate.CanBeGiftWrapped != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.CanBeGiftWrapped);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.CanBeGiftWrapped, "");
                                }
                                if (currentTemplate.IsDiscontinued != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.IsDiscontinued);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.IsDiscontinued, "");
                                }
                                if (currentTemplate.LaunchDate != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.LaunchDate);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.LaunchDate, "");
                                }
                                if (currentTemplate.MerchantShippingGroup != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.MerchantShippingGroup);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.MerchantShippingGroup, "");
                                }
                                if (currentTemplate.ShippingWeight != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ShippingWeight);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ShippingWeight, "");
                                }
                                if (currentTemplate.WeightUnitOfMeasure != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.WeightUnitOfMeasure);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.WeightUnitOfMeasure, "");
                                }
                                if (currentTemplate.BrowseNode != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.BrowseNode);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.BrowseNode, "");
                                }
                                if (currentTemplate.SearchTerms != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SearchTerms);
                                    cell.SetCellValue(product.Description);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.SearchTerms, product.Description);
                                }
                                if (currentTemplate.Features1 != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Features1);
                                    cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[0].Keyword.Keyword);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Features1, product.Keywords == null ? "" : product.Keywords.ToList()[0].Keyword.Keyword);
                                }
                                if (currentTemplate.Features2 != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Features2);
                                    cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[1].Keyword.Keyword);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Features2, product.Keywords == null ? "" : product.Keywords.ToList()[1].Keyword.Keyword);
                                }
                                if (currentTemplate.Features3 != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Features3);
                                    cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[2].Keyword.Keyword);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Features3, product.Keywords == null ? "" : product.Keywords.ToList()[2].Keyword.Keyword);
                                }
                                if (currentTemplate.Features4 != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Features4);
                                    cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[3].Keyword.Keyword);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Features4, product.Keywords == null ? "" : product.Keywords.ToList()[3].Keyword.Keyword);
                                }
                                if (currentTemplate.Features5 != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Features5);
                                    cell.SetCellValue(product.Keywords == null ? "" : product.Keywords.ToList()[4].Keyword.Keyword);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Features5, product.Keywords == null ? "" : product.Keywords.ToList()[4].Keyword.Keyword);
                                }
                                if (currentTemplate.MainImgUrl != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.MainImgUrl);
                                    cell.SetCellValue(Path.Combine(Request.Host.Value, listing.DesignURL));
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.MainImgUrl, Path.Combine(Request.Host.Value, listing.DesignURL));
                                }
                                if (currentTemplate.OtherImgUrl != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.OtherImgUrl);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.OtherImgUrl, "");
                                }
                                if (currentTemplate.OtherImgUrl2 != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.OtherImgUrl2);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.OtherImgUrl2, "");
                                }
                                if (currentTemplate.OtherImgUrl3 != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.OtherImgUrl3);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.OtherImgUrl3, "");
                                }
                                if (currentTemplate.SwatchImgUrl != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SwatchImgUrl);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.SwatchImgUrl, "");
                                }
                                if (currentTemplate.FulfillmentCentreId != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.FulfillmentCentreId);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.FulfillmentCentreId, "");
                                }
                                if (currentTemplate.PackageLength != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.PackageLength);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.PackageLength, "");
                                }
                                if (currentTemplate.PackageWidth != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.PackageWidth);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.PackageWidth, "");
                                }
                                if (currentTemplate.PackageHeight != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.PackageHeight);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.PackageHeight, "");
                                }
                                if (currentTemplate.PackageLengthUnitOfMeasure != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.PackageLengthUnitOfMeasure);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.PackageLengthUnitOfMeasure, "");
                                }
                                if (currentTemplate.PackageWeight != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.PackageWeight);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.PackageWeight, "");
                                }
                                if (currentTemplate.PackageWeightUnitOfMeasure != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.PackageWeightUnitOfMeasure);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.PackageWeightUnitOfMeasure, "");
                                }
                                if (currentTemplate.PackageDimensionsUnitOfMeasure != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.PackageDimensionsUnitOfMeasure);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.PackageDimensionsUnitOfMeasure, "");
                                }
                                if (currentTemplate.Parentage != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Parentage);
                                    cell.SetCellValue("child");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Parentage, "child");
                                }
                                if (currentTemplate.ParentSKU != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ParentSKU);
                                    cell.SetCellValue(account.Name + " - " + product.AmazonProductID);
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ParentSKU, account.Name + " - " + product.AmazonProductID);
                                }
                                if (currentTemplate.VariationTheme != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.VariationTheme);
                                    cell.SetCellValue("size-color");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.VariationTheme, "size-color");
                                }
                                if (currentTemplate.CountryOfOrigin != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.CountryOfOrigin);
                                    cell.SetCellValue("Lithuania");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.CountryOfOrigin, "Lithuania");
                                }
                                if (currentTemplate.ColourMap != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ColourMap);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ColourMap, "");
                                }
                                if (currentTemplate.Colour != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Colour);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Colour, "");
                                }
                                if (currentTemplate.SizeMap != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SizeMap);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Colour, "");
                                }
                                if (currentTemplate.Size != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Size);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Size, "");
                                }
                                if (currentTemplate.MaterialComposition != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.MaterialComposition);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.MaterialComposition, "");
                                }
                                if (currentTemplate.OuterMaterialType != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.OuterMaterialType);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.OuterMaterialType, "");
                                }
                                if (currentTemplate.InnerMaterialType != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.InnerMaterialType);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.InnerMaterialType, "");
                                }
                                if (currentTemplate.SeasonAndCollectionYear != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SeasonAndCollectionYear);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.SeasonAndCollectionYear, "");
                                }
                                if (currentTemplate.ProductCareInstructions != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ProductCareInstructions);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ProductCareInstructions, "");
                                }
                                if (currentTemplate.ModelName != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ModelName);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ModelName, "");
                                }
                                if (currentTemplate.Department != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.Department);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.Department, "");
                                }
                                if (currentTemplate.AdultFlag != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.AdultFlag);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.AdultFlag, "");
                                }
                                if (currentTemplate.ItemShape != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ItemShape);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ItemShape, "");
                                }
                                if (currentTemplate.OccasionDescription != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.OccasionDescription);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.OccasionDescription, "");
                                }
                                if (currentTemplate.StyleName != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.StyleName);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.StyleName, "");
                                }
                                if (currentTemplate.SleeveType != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SleeveType);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.SleeveType, "");
                                }
                                if (currentTemplate.ItemLength != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ItemLength);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ItemLength, "");
                                }
                                if (currentTemplate.BraCupSize != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.BraCupSize);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.BraCupSize, "");
                                }
                                if (currentTemplate.BraBandSize != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.BraBandSize);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.BraBandSize, "");
                                }
                                if (currentTemplate.BraBandSizeUnit != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.BraBandSizeUnit);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.BraBandSizeUnit, "");
                                }
                                if (currentTemplate.SpecialFeatures != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SpecialFeatures);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.SpecialFeatures, "");
                                }
                                if (currentTemplate.OpacityTransparency != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.OpacityTransparency);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.OpacityTransparency, "");
                                }
                                if (currentTemplate.ClosureType != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.ClosureType);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.ClosureType, "");
                                }
                                if (currentTemplate.BikiniTopStyle != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.BikiniTopStyle);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.BikiniTopStyle, "");
                                }
                                if (currentTemplate.SwimwearBottomStyle != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.SwimwearBottomStyle);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.SwimwearBottomStyle, "");
                                }
                                if (currentTemplate.PatternDescription != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.PatternDescription);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.PatternDescription, "");
                                }
                                if (currentTemplate.CollarStyle != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.CollarStyle);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.CollarStyle, "");
                                }
                                if (currentTemplate.FittingType != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.FittingType);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.FittingType, "");
                                }
                                if (currentTemplate.NeckStyle != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.NeckStyle);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.NeckStyle, "");
                                }
                                if (currentTemplate.JeansLengthUnitOfMeasure != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.JeansLengthUnitOfMeasure);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.JeansLengthUnitOfMeasure, "");
                                }
                                if (currentTemplate.JeansLengthInches != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.JeansLengthInches);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.JeansLengthInches, "");
                                }
                                if (currentTemplate.JeansWidthUnitOfMeasure != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.JeansWidthUnitOfMeasure);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.JeansWidthUnitOfMeasure, "");
                                }
                                if (currentTemplate.JeansWidthInches != null)
                                {
                                    cell = row.CreateCell((int)currentTemplate.JeansWidthInches);
                                    cell.SetCellValue("");
                                    //worksheet.SetValue(currentRow, (int)currentTemplate.JeansWidthInches, "");
                                }

                                currentRow++;
                            }
                        }



                        string generatedPath = templatePath.Replace("Templates", "Temp");

                        FileStream xfile = new FileStream(generatedPath, FileMode.Create, System.IO.FileAccess.Write);
                        workbook.Write(xfile);
                        xfile.Close();

                        //FileInfo generatedFile = new FileInfo(generatedPath);
                        //pck.SaveAs(generatedFile);
                        filenamesAndUrls[currentTemplate.Name+Path.GetExtension(generatedPath)] = generatedPath;
                    }

                    //pck.Save();
                    //filenamesAndUrls[CurrentMarketplace.Name] = templatePath;

                }

            }
            var memoryStream = new MemoryStream();
            ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

            // loop over a series of Azure blobs which contain text
            foreach (var kvp in filenamesAndUrls){

                var fileStream = new FileStream(kvp.Value,FileMode.Open);
                var zipEntry = archive.CreateEntry(kvp.Key);

                using (Stream zipStream = zipEntry.Open())
                {
                    fileStream.CopyTo(zipStream);
                }
                fileStream.Dispose();
                
            }

            archive.Dispose();
            memoryStream.Seek(0, SeekOrigin.Begin);

            FileStreamResult result = new FileStreamResult(memoryStream, "application/zip")
            {
                FileDownloadName = "Generated.zip"
            };

            // loop over a series of Azure blobs which contain text
            foreach (var kvp in filenamesAndUrls)
            {
                System.IO.File.Delete(kvp.Value);
            }

            return result;
        }
    }

    
}
