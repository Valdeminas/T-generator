using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Dump;

namespace T_generator.Controllers.Amazon.Data.Dump
{
    public class AmazonKeywordsController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonKeywordsController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonKeywords
        public async Task<IActionResult> Index()
        {
            return View(await _context.AmazonKeywords.ToListAsync());
        }

        // GET: AmazonKeywords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonKeyword = await _context.AmazonKeywords.SingleOrDefaultAsync(m => m.AmazonKeywordID == id);
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
        public async Task<IActionResult> Create([Bind("AmazonKeywordID,Keyword")] AmazonKeyword amazonKeyword)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonKeyword);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonKeyword);
        }

        // GET: AmazonKeywords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonKeyword = await _context.AmazonKeywords.SingleOrDefaultAsync(m => m.AmazonKeywordID == id);
            if (amazonKeyword == null)
            {
                return NotFound();
            }
            return View(amazonKeyword);
        }

        // POST: AmazonKeywords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonKeywordID,Keyword")] AmazonKeyword amazonKeyword)
        {
            if (id != amazonKeyword.AmazonKeywordID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonKeyword);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonKeywordExists(amazonKeyword.AmazonKeywordID))
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
            return View(amazonKeyword);
        }

        // GET: AmazonKeywords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonKeyword = await _context.AmazonKeywords.SingleOrDefaultAsync(m => m.AmazonKeywordID == id);
            if (amazonKeyword == null)
            {
                return NotFound();
            }

            return View(amazonKeyword);
        }

        // POST: AmazonKeywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonKeyword = await _context.AmazonKeywords.SingleOrDefaultAsync(m => m.AmazonKeywordID == id);
            if (!RelatedProductExists(id))
            {
                _context.AmazonKeywords.Remove(amazonKeyword);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["RelatedEntriesExistError"] = "This object has other entries using it."
                    + Environment.NewLine
                    + "Delete the entries using this object first, and then try to delete this object.";
                return View(amazonKeyword);
            }
        }

        private bool AmazonKeywordExists(int id)
        {
            return _context.AmazonKeywords.Any(e => e.AmazonKeywordID == id);
        }

        private bool RelatedProductExists(int id)
        {
            return _context.KeywordAssignment.Any(e => e.KeywordID == id);
        }
    }
}
