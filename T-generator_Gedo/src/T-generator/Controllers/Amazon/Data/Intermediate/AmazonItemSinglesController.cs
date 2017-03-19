using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Objects.Item.Children;
using T_generator.Services.Amazon;

namespace T_generator.Controllers.Amazon.Data.Intermediate
{
    public class AmazonItemSinglesController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonItemSinglesController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonItemSingles
        public async Task<IActionResult> Index(int? page)
        {
            //var amazonContext = _context.AmazonItemSingles.Include(a => a.AmazonBrowseNode).Include(a => a.AmazonCountry).Include(a => a.AmazonProduct);
            //return View(await amazonContext.ToListAsync());

            int itemsPerPage = 35;
            var items = from s in _context.AmazonItemSingles.Include(a => a.AmazonBrowseNode).Include(a => a.AmazonCountry).Include(a => a.AmazonProduct)
            select s;
            return View(await PaginatedList<AmazonItemSingle>.CreateAsync(items.AsNoTracking(), page ?? 1, itemsPerPage));
        }

        // GET: AmazonItemSingles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonItemSingle = await _context.AmazonItemSingles.SingleOrDefaultAsync(m => m.AmazonItemID == id);
            if (amazonItemSingle == null)
            {
                return NotFound();
            }

            return View(amazonItemSingle);
        }

        // GET: AmazonItemSingles/Create
        public IActionResult Create()
        {
            ViewData["AmazonBrowseNodeID"] = new SelectList(_context.AmazonBrowseNodes, "AmazonBrowseNodeID", "AmazonBrowseNodeID");
            ViewData["AmazonCountryID"] = new SelectList(_context.AmazonCountries, "AmazonCountryID", "AmazonCountryID");
            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "AmazonProductID");
            return View();
        }

        // POST: AmazonItemSingles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonItemID,AmazonBrowseNodeID,AmazonCountryID,AmazonProductID,Price,Quantity")] AmazonItemSingle amazonItemSingle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonItemSingle);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AmazonBrowseNodeID"] = new SelectList(_context.AmazonBrowseNodes, "AmazonBrowseNodeID", "AmazonBrowseNodeID", amazonItemSingle.AmazonBrowseNodeID);
            ViewData["AmazonCountryID"] = new SelectList(_context.AmazonCountries, "AmazonCountryID", "AmazonCountryID", amazonItemSingle.AmazonCountryID);
            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "AmazonProductID", amazonItemSingle.AmazonProductID);
            return View(amazonItemSingle);
        }

        // GET: AmazonItemSingles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonItemSingle = await _context.AmazonItemSingles.SingleOrDefaultAsync(m => m.AmazonItemID == id);
            if (amazonItemSingle == null)
            {
                return NotFound();
            }
            ViewData["AmazonBrowseNodeID"] = new SelectList(_context.AmazonBrowseNodes, "AmazonBrowseNodeID", "AmazonBrowseNodeID", amazonItemSingle.AmazonBrowseNodeID);
            ViewData["AmazonCountryID"] = new SelectList(_context.AmazonCountries, "AmazonCountryID", "AmazonCountryID", amazonItemSingle.AmazonCountryID);
            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "AmazonProductID", amazonItemSingle.AmazonProductID);
            return View(amazonItemSingle);
        }

        // POST: AmazonItemSingles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonItemID,AmazonBrowseNodeID,AmazonCountryID,AmazonProductID,Price,Quantity")] AmazonItemSingle amazonItemSingle)
        {
            if (id != amazonItemSingle.AmazonItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonItemSingle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonItemSingleExists(amazonItemSingle.AmazonItemID))
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
            ViewData["AmazonBrowseNodeID"] = new SelectList(_context.AmazonBrowseNodes, "AmazonBrowseNodeID", "AmazonBrowseNodeID", amazonItemSingle.AmazonBrowseNodeID);
            ViewData["AmazonCountryID"] = new SelectList(_context.AmazonCountries, "AmazonCountryID", "AmazonCountryID", amazonItemSingle.AmazonCountryID);
            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "AmazonProductID", amazonItemSingle.AmazonProductID);
            return View(amazonItemSingle);
        }

        // GET: AmazonItemSingles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonItemSingle = await _context.AmazonItemSingles.SingleOrDefaultAsync(m => m.AmazonItemID == id);
            if (amazonItemSingle == null)
            {
                return NotFound();
            }

            return View(amazonItemSingle);
        }

        // POST: AmazonItemSingles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonItemSingle = await _context.AmazonItemSingles.SingleOrDefaultAsync(m => m.AmazonItemID == id);
            _context.AmazonItemSingles.Remove(amazonItemSingle);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AmazonItemSingleExists(int id)
        {
            return _context.AmazonItemSingles.Any(e => e.AmazonItemID == id);
        }
    }
}
