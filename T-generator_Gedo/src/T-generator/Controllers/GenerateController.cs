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
                        worksheet.SetValue(currentRow, 1, product.AmazonProductID);
                        worksheet.SetValue(currentRow, 4, product.Name);
                        worksheet.SetValue(currentRow, 5, account.Name);
                        worksheet.SetValue(currentRow, 6, product.AmazonTypeID);
                        worksheet.SetValue(currentRow, 7, product.Description);
                        worksheet.SetValue(currentRow, 10, product.AmazonProductID);
                        worksheet.SetValue(currentRow, 31, "Keywords");

                        currentRow++;

                    }

                    pck.Save();

                    filenamesAndUrls[CurrentMarketplace.Name] = templatePath;

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
            }

            archive.Dispose();
            memoryStream.Seek(0, SeekOrigin.Begin);

            FileStreamResult result = new FileStreamResult(memoryStream, "application/zip");
            result.FileDownloadName = "Generated.zip";

            return result;
        }
    }

    
}
