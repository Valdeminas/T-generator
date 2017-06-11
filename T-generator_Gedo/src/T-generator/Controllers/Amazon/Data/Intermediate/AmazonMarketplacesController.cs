using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Intermediate;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using T_generator.Services.Amazon;

namespace T_generator.Controllers.Amazon.Data.Intermediate
{
    public class AmazonMarketplacesController : Controller
    {
        private const string TEMPLATE_DIR = "uploads/Templates";

        private readonly AmazonContext _context;
        private IHostingEnvironment _environment;

        public AmazonMarketplacesController(AmazonContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AmazonMarketplaces
        public async Task<IActionResult> Index(int? page)
        {
            //var amazonContext = _context.AmazonMarketplaces.Include(a => a.AmazonCurrency);
            //return View(await amazonContext.ToListAsync());

            int itemsPerPage = 35;
            var items = from s in _context.AmazonMarketplaces.Include(a => a.AmazonCurrency)
                        select s;
            return View(await PaginatedList<AmazonMarketplace>.CreateAsync(items.AsNoTracking(), page ?? 1, itemsPerPage));
        }

        // GET: AmazonMarketplaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonMarketplace = await _context.AmazonMarketplaces.SingleOrDefaultAsync(m => m.AmazonMarketplaceID == id);
            if (amazonMarketplace == null)
            {
                return NotFound();
            }

            return View(amazonMarketplace);
        }

        // GET: AmazonMarketplaces/Create
        public IActionResult Create()
        {
            ViewData["AmazonCurrencyID"] = new SelectList(_context.AmazonCurrencies, "AmazonCurrencyID", "AmazonCurrencyID");
            return View();
        }

        // POST: AmazonMarketplaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonMarketplaceID,AdultFlag,AmazonCurrencyID,BikiniTopStyle,BraBandSize,BraBandSizeUnit,BraCupSize,BrandName,BrowseNode,CanBeGiftMessaged,CanBeGiftWrapped,ClosureType,ClothingType,CollarStyle,Colour,ColourMap,CountryOfOrigin,Department,Features1,Features2,Features3,Features4,Features5,FittingType,FulfillmentCentreId,FulfillmentLatency,GtinExemptionReason,InnerMaterialType,IsDiscontinued,ItemLength,ItemSKU,ItemShape,JeansLengthInches,JeansLengthUnitOfMeasure,JeansWidthInches,JeansWidthUnitOfMeasure,LaunchDate,MainImgUrl,ManufacturerPartNumber,MaterialComposition,MaxAggrShipQuant,MerchantShippingGroup,ModelName,ModelNumber,Name,NeckStyle,NumberOfItems,OccasionDescription,OpacityTransparency,OtherImgUrl,OuterMaterialType,PackageDimensionsUnitOfMeasure,PackageHeight,PackageLength,PackageLengthUnitOfMeasure,PackageQuantity,PackageWeight,PackageWeightUnitOfMeasure,PackageWidth,ParentSKU,Parentage,PatternDescription,Prefix,ProductCareInstructions,ProductDescription,ProductID,ProductName,ProductType,Quantity,RelatedProductID,RelatedProductType,RelationshipType,SaleEndDate,SaleFromDate,SalePrice,SearchTerms,SeasonAndCollectionYear,SheetNumber,ShippingWeight,Size,SizeMap,SleeveType,SpecialFeatures,StandardPrice,StartingRow,StyleName,SwatchImgUrl,SwimwearBottomStyle,TemplateURL,UpdateDelete,VariationTheme,WeightUnitOfMeasure")] AmazonMarketplace amazonMarketplace, IFormFile TemplateURL)
        {
            if (ModelState.IsValid && TemplateURL != null)
            {
                _context.Add(amazonMarketplace);
                _context.SaveChanges();
                var fullUploadPath = Path.Combine(_environment.WebRootPath, TEMPLATE_DIR);
                var extension = TemplateURL.FileName.Split('.').Last();
                if (extension != "xlsx")
                {
                    ViewData["AmazonCurrencyID"] = new SelectList(_context.AmazonCurrencies, "AmazonCurrencyID", "Currency", amazonMarketplace.AmazonCurrencyID);
                    ViewData["Error"] = "Must be .xlsx file.";
                    return View(amazonMarketplace);
                }
                var filename = amazonMarketplace.AmazonMarketplaceID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extension;
                fullUploadPath = Path.Combine(fullUploadPath, filename);
                using (var fileStream = new FileStream(fullUploadPath, FileMode.Create))
                {
                    await TemplateURL.CopyToAsync(fileStream);
                    amazonMarketplace.TemplateURL = Path.Combine(TEMPLATE_DIR, filename);
                }

                // _context.Add(amazonDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AmazonCurrencyID"] = new SelectList(_context.AmazonCurrencies, "AmazonCurrencyID", "Currency", amazonMarketplace.AmazonCurrencyID);
            return View(amazonMarketplace);
        }

        // GET: AmazonMarketplaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonMarketplace = await _context.AmazonMarketplaces.SingleOrDefaultAsync(m => m.AmazonMarketplaceID == id);
            if (amazonMarketplace == null)
            {
                return NotFound();
            }
            ViewData["AmazonCurrencyID"] = new SelectList(_context.AmazonCurrencies, "AmazonCurrencyID", "AmazonCurrencyID", amazonMarketplace.AmazonCurrencyID);
            return View(amazonMarketplace);
        }

        // POST: AmazonMarketplaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonMarketplaceID,AdultFlag,AmazonCurrencyID,BikiniTopStyle,BraBandSize,BraBandSizeUnit,BraCupSize,BrandName,BrowseNode,CanBeGiftMessaged,CanBeGiftWrapped,ClosureType,ClothingType,CollarStyle,Colour,ColourMap,CountryOfOrigin,Department,Features1,Features2,Features3,Features4,Features5,FittingType,FulfillmentCentreId,FulfillmentLatency,GtinExemptionReason,InnerMaterialType,IsDiscontinued,ItemLength,ItemSKU,ItemShape,JeansLengthInches,JeansLengthUnitOfMeasure,JeansWidthInches,JeansWidthUnitOfMeasure,LaunchDate,MainImgUrl,ManufacturerPartNumber,MaterialComposition,MaxAggrShipQuant,MerchantShippingGroup,ModelName,ModelNumber,Name,NeckStyle,NumberOfItems,OccasionDescription,OpacityTransparency,OtherImgUrl,OuterMaterialType,PackageDimensionsUnitOfMeasure,PackageHeight,PackageLength,PackageLengthUnitOfMeasure,PackageQuantity,PackageWeight,PackageWeightUnitOfMeasure,PackageWidth,ParentSKU,Parentage,PatternDescription,Prefix,ProductCareInstructions,ProductDescription,ProductID,ProductName,ProductType,Quantity,RelatedProductID,RelatedProductType,RelationshipType,SaleEndDate,SaleFromDate,SalePrice,SearchTerms,SeasonAndCollectionYear,SheetNumber,ShippingWeight,Size,SizeMap,SleeveType,SpecialFeatures,StandardPrice,StartingRow,StyleName,SwatchImgUrl,SwimwearBottomStyle,TemplateURL,UpdateDelete,VariationTheme,WeightUnitOfMeasure")] AmazonMarketplace amazonMarketplace, IFormFile TemplateURL, string oldURL)
        {
            if (id != amazonMarketplace.AmazonMarketplaceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (TemplateURL != null)
                    {
                        var fullUploadPath = Path.Combine(_environment.WebRootPath, TEMPLATE_DIR);
                        var extension = TemplateURL.FileName.Split('.').Last();
                        if (extension != "xlsx")
                        {
                            ViewData["Error"] = "Must be .xlsx file.";
                            ViewData["AmazonCurrencyID"] = new SelectList(_context.AmazonCurrencies, "AmazonCurrencyID", "Currency", amazonMarketplace.AmazonCurrencyID);
                            amazonMarketplace.TemplateURL = oldURL;
                            return View(amazonMarketplace);
                        }
                        var filename = amazonMarketplace.AmazonMarketplaceID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extension;
                        fullUploadPath = Path.Combine(fullUploadPath, filename);
                        using (var fileStream = new FileStream(fullUploadPath, FileMode.Create))
                        {
                            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, oldURL));
                            await TemplateURL.CopyToAsync(fileStream);
                            amazonMarketplace.TemplateURL = Path.Combine(TEMPLATE_DIR, filename);
                        }
                    }
                    else
                    {
                        amazonMarketplace.TemplateURL = oldURL;
                    }
                    _context.Update(amazonMarketplace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonMarketplaceExists(amazonMarketplace.AmazonMarketplaceID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["AmazonCurrencyID"] = new SelectList(_context.AmazonCurrencies, "AmazonCurrencyID", "Currency", amazonMarketplace.AmazonCurrencyID);
            return View(amazonMarketplace);
        }

        // GET: AmazonMarketplaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonMarketplace = await _context.AmazonMarketplaces.SingleOrDefaultAsync(m => m.AmazonMarketplaceID == id);
            if (amazonMarketplace == null)
            {
                return NotFound();
            }

            return View(amazonMarketplace);
        }

        // POST: AmazonMarketplaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonMarketplace = await _context.AmazonMarketplaces.SingleOrDefaultAsync(m => m.AmazonMarketplaceID == id);
            _context.AmazonMarketplaces.Remove(amazonMarketplace);
            await _context.SaveChangesAsync();
            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, amazonMarketplace.TemplateURL));
            return RedirectToAction("Index");
        }

        private bool AmazonMarketplaceExists(int id)
        {
            return _context.AmazonMarketplaces.Any(e => e.AmazonMarketplaceID == id);
        }
    }
}
