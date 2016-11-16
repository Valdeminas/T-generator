using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Intermediate;
using T_generator.AmazonViewModel;
using T_generator.Models.Amazon.Data.Dump;
using Microsoft.CodeAnalysis;
using T_generator.Models.Amazon.Data.JoinTables;
using T_generator.Models.Amazon.Data.Basic;

namespace T_generator.Controllers.Amazon.Data.Intermediate
{
    public class AmazonProductsController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonProductsController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonProducts
        public async Task<IActionResult> Index()
        {
            var amazonContext = _context.AmazonProducts.Include(a => a.AmazonType);
            return View(await amazonContext.ToListAsync());
        }

        // GET: AmazonProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonProduct = await _context.AmazonProducts
                .Include(i=>i.AmazonType)
                .Include(i => i.Keywords)
                .ThenInclude(i => i.Keyword)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AmazonProductID == id);
            if (amazonProduct == null)
            {
                return NotFound();
            }
            PopulateAssignedKeywordData(amazonProduct);
            return View(amazonProduct);
        }

        // GET: AmazonProducts/Create
        public IActionResult Create()
        {
            var amazonProduct = new AmazonProduct();
            amazonProduct.Keywords = new List<KeywordAssignment>();
            ViewData["AmazonSizeID"] = new MultiSelectList(_context.AmazonSizes, "AmazonSizeID", "Name");
            ViewData["AmazonTypeID"] = new SelectList(_context.AmazonTypes, "AmazonTypeID", "Name");
            return View();
        }


        // POST: AmazonProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonTypeID,Description,Name,Prefix")] AmazonProduct amazonProduct, IList<AssignedKeywordsViewModel> keywords, List<int> Sizes)
        {
            amazonProduct.Sizes = new List<ProductSizes>();
            amazonProduct.Keywords = new List<KeywordAssignment>();
            UpdateProductSizes(Sizes,amazonProduct);
            UpdateProductKeywords(keywords, amazonProduct);
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(amazonProduct);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch(DbUpdateException  ex )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError(ex.ToString(), "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            ViewData["AmazonSizeID"] = new MultiSelectList(_context.AmazonSizes, "AmazonSizeID", "Name");
            ViewData["AmazonTypeID"] = new SelectList(_context.AmazonTypes, "AmazonTypeID", "Name", amazonProduct.AmazonTypeID);
            return View(amazonProduct);
        }

        // GET: AmazonProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonProduct = await _context.AmazonProducts
                .Include(i=>i.Sizes)
                .Include(i => i.Keywords)
                .ThenInclude(i=>i.Keyword)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AmazonProductID == id);
            if (amazonProduct == null)
            {
                return NotFound();
            }
            ViewData["AmazonSizeID"] = new MultiSelectList(_context.AmazonSizes, "AmazonSizeID", "Name", new[] { "1", "2" });// amazonProduct.Sizes.Select(i=>i.SizeID).ToArray());
            ViewData["AmazonTypeID"] = new SelectList(_context.AmazonTypes, "AmazonTypeID", "Name", amazonProduct.AmazonTypeID);
            PopulateAssignedKeywordData(amazonProduct);
            return View(amazonProduct);
        }

        // POST: AmazonProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, IList<AssignedKeywordsViewModel> keywords)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToUpdate = await _context.AmazonProducts
                .Include(i => i.AmazonType)
                .Include(i => i.Keywords)
                .SingleOrDefaultAsync(m => m.AmazonProductID == id);

            if (await TryUpdateModelAsync<AmazonProduct>(
                    productToUpdate,
                    "",
                    i => i.Name, i => i.Prefix, i => i.Description, i => i.AmazonTypeID))
            {
                UpdateProductKeywords(keywords, productToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction("Index");
            }

            ViewData["AmazonTypeID"] = new SelectList(_context.AmazonTypes, "AmazonTypeID", "Name", productToUpdate.AmazonTypeID);
            return View(productToUpdate);
        }

        // GET: AmazonProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonProduct = await _context.AmazonProducts
                .Include(i => i.AmazonType)
                .Include(i => i.Keywords)
                .ThenInclude(i => i.Keyword)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AmazonProductID == id);
            if (amazonProduct == null)
            {
                return NotFound();
            }
            PopulateAssignedKeywordData(amazonProduct);
            return View(amazonProduct);
        }

        // POST: AmazonProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonProduct = await _context.AmazonProducts.Include(i=>i.Keywords).SingleOrDefaultAsync(m => m.AmazonProductID == id);
            _context.AmazonProducts.Remove(amazonProduct);
            foreach(var keyword in amazonProduct.Keywords)
            {
                if (_context.KeywordAssignment.Where(a=>a.ProductID!=amazonProduct.AmazonProductID).Count(m => m.KeywordID == keyword.KeywordID) == 0)
                {
                    var keywordToRemove = await _context.AmazonKeywords.SingleOrDefaultAsync(m => m.AmazonKeywordID == keyword.KeywordID);
                    _context.AmazonKeywords.Remove(keywordToRemove);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AmazonProductExists(int id)
        {
            return _context.AmazonProducts.Any(e => e.AmazonProductID == id);
        }

        public IActionResult UpdateKeywords(string term)
        {
            List<AmazonKeyword> KeywordList = new List<AmazonKeyword>();
            foreach (AmazonKeyword keyword in _context.AmazonKeywords)
            {
                if (keyword.Keyword.ToLower().Contains(term.ToLower()))
                {
                    KeywordList.Add(keyword);
                }

            }
            return Json(KeywordList);
        }

        private void PopulateAssignedKeywordData(AmazonProduct amazonProduct)
        {
            var productKeywords =  amazonProduct
                                        .Keywords
                                        .ToDictionary(c => c.Order,
                                                    c => c.Keyword.Keyword);
            for (int i= 0;i<5;i++)
            {
                if (productKeywords.Keys.Contains(i))
                {
                    ViewData[i.ToString()] = productKeywords[i];
                }
                else
                {
                    ViewData[i.ToString()] = "";
                }
            }
        }

        private void UpdateProductKeywords(IList<AssignedKeywordsViewModel> selectedKeywords, AmazonProduct productToUpdate)
        {
            var keywordsFromDb = _context.AmazonKeywords
                                .ToDictionary(c => c.Keyword,
                                              c => c.AmazonKeywordID);
            for (int i = 0; i < selectedKeywords.Count; i++)
            {
                KeywordAssignment keywordToRemove = productToUpdate.Keywords.SingleOrDefault(a => a.Order == i);
                if (keywordToRemove != null)
                {
                    _context.Remove(keywordToRemove);
                    _context.SaveChanges();
                }

                if (selectedKeywords[i].Keyword != null)
                {                  
                    if (keywordsFromDb.Keys.Contains(selectedKeywords[i].Keyword))
                    {
                        productToUpdate.Keywords.Add(new KeywordAssignment { ProductID = productToUpdate.AmazonProductID, KeywordID = keywordsFromDb[selectedKeywords[i].Keyword], Order = i });
                    }
                    else
                    {
                        AmazonKeyword newKeyword = new AmazonKeyword { Keyword = selectedKeywords[i].Keyword };
                        _context.AmazonKeywords.Add(newKeyword);
                        _context.SaveChanges();
                        productToUpdate.Keywords.Add(new KeywordAssignment { ProductID = productToUpdate.AmazonProductID, KeywordID = newKeyword.AmazonKeywordID, Order = i });
                        keywordsFromDb[newKeyword.Keyword] = newKeyword.AmazonKeywordID;
                    }
                }
            }
        }

        private void UpdateProductSizes(List<int> Sizes,AmazonProduct productToUpdate)
        {
            foreach(var sizeid in Sizes)
            {
                productToUpdate.Sizes.Add(new ProductSizes { ProductID=productToUpdate.AmazonProductID,SizeID=sizeid});
            }
        }

    }
}
