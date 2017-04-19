using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Services.Amazon;

namespace T_generator.Controllers.Amazon.Data.Dump
{
    public class AmazonCategoriesController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonCategoriesController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonKeywords
        public async Task<IActionResult> Index(int? page)
        {
            int itemsPerPage = 35;
            var items = from s in _context.AmazonCategories
                        select s;
            return View(await PaginatedList<AmazonCategory>.CreateAsync(items.AsNoTracking(), page ?? 1, itemsPerPage));
        }

        // GET: AmazonKeywords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonKeyword = await _context.AmazonCategories.SingleOrDefaultAsync(m => m.AmazonCategoryID == id);
            if (amazonKeyword == null)
            {
                return NotFound();
            }

            return View(amazonKeyword);
        }

        // GET: AmazonKeywords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmazonKeywords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonCategoryID,Category")] AmazonCategory amazonCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonCategory);
        }

        // GET: AmazonKeywords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonCategory = await _context.AmazonCategories.SingleOrDefaultAsync(m => m.AmazonCategoryID == id);
            if (amazonCategory == null)
            {
                return NotFound();
            }
            return View(amazonCategory);
        }

        // POST: AmazonKeywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonCategoryID,Category")] AmazonCategory amazonCategory)
        {
            if (id != amazonCategory.AmazonCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonCategoryExists(amazonCategory.AmazonCategoryID))
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
            return View(amazonCategory);
        }

        // GET: AmazonKeywords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonCategory = await _context.AmazonCategories.SingleOrDefaultAsync(m => m.AmazonCategoryID == id);
            if (amazonCategory == null)
            {
                return NotFound();
            }

            return View(amazonCategory);
        }

        // POST: AmazonKeywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonCategory = await _context.AmazonCategories.SingleOrDefaultAsync(m => m.AmazonCategoryID == id);
            if (!RelatedDesignExists(id))
            {
                _context.AmazonCategories.Remove(amazonCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["RelatedEntriesExistError"] = "This object has other entries using it."
                    + Environment.NewLine
                    + "Delete the entries using this object first, and then try to delete this object.";
                return View(amazonCategory);
            }
        }

        private bool AmazonCategoryExists(int id)
        {
            return _context.AmazonCategories.Any(e => e.AmazonCategoryID == id);
        }

        private bool RelatedDesignExists(int id)
        {
            return _context.AmazonDesigns.Any(e => e.AmazonCategoryID == id);
        }
    }
}
