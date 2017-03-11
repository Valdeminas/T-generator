using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Basic;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Services.Amazon;

namespace T_generator.Controllers.Amazon.Data.Basic
{
    public class AmazonDesignsController : Controller
    {
        private const string DESIGN_DIR= "uploads/Designs";

        private readonly AmazonContext _context;
        private IHostingEnvironment _environment;

        public AmazonDesignsController(AmazonContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AmazonDesigns
        public async Task<IActionResult> Index(int? page)
        {
            int itemsPerPage = 35;
            var items = from s in _context.AmazonDesigns
                        select s;
            return View(await PaginatedList<AmazonDesign>.CreateAsync(items.AsNoTracking(), page ?? 1, itemsPerPage));
        }

        // GET: AmazonDesigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonDesign = await _context.AmazonDesigns
                .Include(a => a.AmazonAccount)
                .Include(a => a.AmazonMarketplace)
                .Include(a => a.AmazonCategory)
                .SingleOrDefaultAsync(m => m.AmazonDesignID == id);

            if (amazonDesign == null)
            {
                return NotFound();
            }

            return View(amazonDesign);
        }

        // GET: AmazonDesigns/Create
        public IActionResult Create()
        {
            ViewData["AmazonAccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "Name");
            ViewData["AmazonMarketplaceID"] = new SelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "Name");
            ViewData["AmazonCategory"] = "";
            return View();
        }

        // POST: AmazonDesigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonDesignID,AmazonAccountID,AmazonMarketplaceID,DesignURL,Name,SearchTags1,SearchTags2,SearchTags3")] AmazonDesign amazonDesign,String Category, IFormFile DesignURL)
        {
            
            if (ModelState.IsValid && DesignURL != null && Category != null)
            {
                UpdateDesignCategories(Category, amazonDesign);            
                _context.Add(amazonDesign);
                _context.SaveChanges();
                    var fullUploadPath = Path.Combine(_environment.WebRootPath, DESIGN_DIR);
                    var extension = DesignURL.FileName.Split('.').Last();
                    var filename = amazonDesign.AmazonDesignID + "." + extension;
                    fullUploadPath = Path.Combine(fullUploadPath, filename);           
                    using (var fileStream = new FileStream(fullUploadPath, FileMode.Create))
                    {
                        await DesignURL.CopyToAsync(fileStream);
                        amazonDesign.DesignURL = Path.Combine(DESIGN_DIR, filename);
                    }
                
               // _context.Add(amazonDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AmazonAccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "Name", amazonDesign.AmazonAccountID);
            ViewData["AmazonMarketplaceID"] = new SelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "Name");
            ViewData["AmazonCategory"] = Category;
            return View(amazonDesign);
        }

        // GET: AmazonDesigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonDesign = await _context.AmazonDesigns.Include(i => i.AmazonCategory).SingleOrDefaultAsync(m => m.AmazonDesignID == id);
            if (amazonDesign == null)
            {
                return NotFound();
            }
            ViewData["AmazonAccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "Name", amazonDesign.AmazonAccountID);
            ViewData["AmazonMarketplaceID"] = new SelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "Name", amazonDesign.AmazonMarketplaceID);
            return View(amazonDesign);
        }

        // POST: AmazonDesigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonDesignID,AmazonAccountID,DesignURL,AmazonMarketplaceID,Name,SearchTags1,SearchTags2,SearchTags3")] AmazonDesign amazonDesign, String Category, IFormFile DesignURL,string oldURL)
        {
            if (id != amazonDesign.AmazonDesignID)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid && Category != null)
            {
                try
                {
                    UpdateDesignCategories(Category, amazonDesign);
                    if (DesignURL != null)
                    {
                        var fullUploadPath = Path.Combine(_environment.WebRootPath, DESIGN_DIR);
                        var extension = DesignURL.FileName.Split('.').Last();
                        var filename = amazonDesign.AmazonDesignID + "." + extension;
                        fullUploadPath = Path.Combine(fullUploadPath, filename);
                        using (var fileStream = new FileStream(fullUploadPath, FileMode.Create))
                        {
                            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, oldURL));
                            await DesignURL.CopyToAsync(fileStream);
                            amazonDesign.DesignURL = Path.Combine(DESIGN_DIR, filename);
                        }
                    }
                    else
                    {
                        amazonDesign.DesignURL = oldURL;
                    }
                    _context.Update(amazonDesign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonDesignExists(amazonDesign.AmazonDesignID))
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

            amazonDesign = await _context.AmazonDesigns
                .Include(i => i.AmazonCategory)
                .SingleOrDefaultAsync(m => m.AmazonDesignID == id);

            ViewData["AmazonAccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "Name", amazonDesign.AmazonAccountID);
            ViewData["AmazonMarketplaceID"] = new SelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "Name", amazonDesign.AmazonMarketplaceID);
            return View(amazonDesign);
        }

        // GET: AmazonDesigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonDesign = await _context.AmazonDesigns
                .Include(a => a.AmazonAccount)
                .Include(a => a.AmazonMarketplace)
                .Include(a => a.AmazonCategory)
                .SingleOrDefaultAsync(m => m.AmazonDesignID == id);

            if (amazonDesign == null)
            {
                return NotFound();
            }

            return View(amazonDesign);
        }

        // POST: AmazonDesigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonDesign = await _context.AmazonDesigns.Include(i => i.AmazonCategory).SingleOrDefaultAsync(m => m.AmazonDesignID == id);
            _context.AmazonDesigns.Remove(amazonDesign);
                if (_context.AmazonCategories.Count(m => m.Category == amazonDesign.AmazonCategory.Category) == 1)
                {
                    var categoryToRemove = await _context.AmazonCategories.SingleOrDefaultAsync(m => m.AmazonCategoryID == amazonDesign.AmazonCategory.AmazonCategoryID);
                    _context.AmazonCategories.Remove(categoryToRemove);
                }
            await _context.SaveChangesAsync();
            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, amazonDesign.DesignURL));
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCategories(string term)
        {
            List<AmazonCategory> CategoryList = new List<AmazonCategory>();
            foreach (AmazonCategory category in _context.AmazonCategories)
            {
                if (category.Category.ToLower().Contains(term.ToLower()))
                {
                    CategoryList.Add(category);
                }

            }
            return Json(CategoryList);
        }

        private void UpdateDesignCategories(String amazonCategory, AmazonDesign amazonDesign)
        {
            var keywordsFromDb = _context.AmazonCategories
                                .ToDictionary(c => c.Category,
                                              c => c.AmazonCategoryID);

                if (amazonCategory != null)
                {
                    if (keywordsFromDb.Keys.Contains(amazonCategory))
                    {
                        amazonDesign.AmazonCategoryID=keywordsFromDb[amazonCategory];
                    }
                    else
                    {
                        AmazonCategory newKeyword = new AmazonCategory { Category = amazonCategory };
                        _context.AmazonCategories.Add(newKeyword);
                        _context.SaveChanges();
                        amazonDesign.AmazonCategoryID = newKeyword.AmazonCategoryID;
                    }
                }
        }

        private bool AmazonDesignExists(int id)
        {
            return _context.AmazonDesigns.Any(e => e.AmazonDesignID == id);
        }
    }
}
