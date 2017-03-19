using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Intermediate;
using T_generator.Services.Amazon;

namespace T_generator.Controllers.Amazon.Data.Intermediate
{
    public class AmazonMarketplacesController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonMarketplacesController(AmazonContext context)
        {
            _context = context;    
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

            var amazonMarketplace = await _context.AmazonMarketplaces.Include(a => a.AmazonCurrency).SingleOrDefaultAsync(m => m.AmazonMarketplaceID == id);
            if (amazonMarketplace == null)
            {
                return NotFound();
            }

            return View(amazonMarketplace);
        }

        // GET: AmazonMarketplaces/Create
        public IActionResult Create()
        {
            ViewData["AmazonCurrencyID"] = new SelectList(_context.AmazonCurrencies, "AmazonCurrencyID", "Currency");
            return View();
        }

        // POST: AmazonMarketplaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonMarketplaceID,AmazonCurrencyID,Name,Prefix")] AmazonMarketplace amazonMarketplace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonMarketplace);
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
            ViewData["AmazonCurrencyID"] = new SelectList(_context.AmazonCurrencies, "AmazonCurrencyID", "Currency", amazonMarketplace.AmazonCurrencyID);
            return View(amazonMarketplace);
        }

        // POST: AmazonMarketplaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonMarketplaceID,AmazonCurrencyID,Name,Prefix")] AmazonMarketplace amazonMarketplace)
        {
            if (id != amazonMarketplace.AmazonMarketplaceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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

            var amazonMarketplace = await _context.AmazonMarketplaces.Include(a => a.AmazonCurrency).SingleOrDefaultAsync(m => m.AmazonMarketplaceID == id);
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
            return RedirectToAction("Index");
        }

        private bool AmazonMarketplaceExists(int id)
        {
            return _context.AmazonMarketplaces.Any(e => e.AmazonMarketplaceID == id);
        }
    }
}
