using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Basic;

namespace T_generator.Controllers.Amazon.Data.Basic
{
    public class AmazonCurrenciesController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonCurrenciesController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonCurrencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.AmazonCurrencies.ToListAsync());
        }

        // GET: AmazonCurrencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonCurrency = await _context.AmazonCurrencies.SingleOrDefaultAsync(m => m.AmazonCurrencyID == id);
            if (amazonCurrency == null)
            {
                return NotFound();
            }

            return View(amazonCurrency);
        }

        // GET: AmazonCurrencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmazonCurrencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonCurrencyID,Currency")] AmazonCurrency amazonCurrency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonCurrency);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonCurrency);
        }

        // GET: AmazonCurrencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonCurrency = await _context.AmazonCurrencies.SingleOrDefaultAsync(m => m.AmazonCurrencyID == id);
            if (amazonCurrency == null)
            {
                return NotFound();
            }
            return View(amazonCurrency);
        }

        // POST: AmazonCurrencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonCurrencyID,Currency")] AmazonCurrency amazonCurrency)
        {
            if (id != amazonCurrency.AmazonCurrencyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonCurrency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonCurrencyExists(amazonCurrency.AmazonCurrencyID))
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
            return View(amazonCurrency);
        }

        // GET: AmazonCurrencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonCurrency = await _context.AmazonCurrencies.SingleOrDefaultAsync(m => m.AmazonCurrencyID == id);
            if (amazonCurrency == null)
            {
                return NotFound();
            }

            return View(amazonCurrency);
        }

        // POST: AmazonCurrencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonCurrency = await _context.AmazonCurrencies.SingleOrDefaultAsync(m => m.AmazonCurrencyID == id);
            if (!RelatedMarketplaceExists(id))
            {               
                _context.AmazonCurrencies.Remove(amazonCurrency);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["RelatedEntriesExistError"] = "This object has other entries using it."
                    + Environment.NewLine
                    +"Delete the entries using this object first, and then try to delete this object.";
                return View(amazonCurrency);
            }
        }

        private bool AmazonCurrencyExists(int id)
        {
            return _context.AmazonCurrencies.Any(e => e.AmazonCurrencyID == id);
        }

        private bool RelatedMarketplaceExists(int id)
        {
            return _context.AmazonMarketplaces.Any(e => e.AmazonCurrencyID == id);
        }
    }
}
