using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.JoinTables;

namespace T_generator.Controllers.Amazon.Data.Intermediate
{
    public class AccountMarketplacesController : Controller
    {
        private readonly AmazonContext _context;

        public AccountMarketplacesController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonMarketplacesController
        public async Task<IActionResult> Index()
        {
            var amazonContext = _context.AccountMarketplaces.Include(a => a.Account).Include(a => a.Marketplace);
            return View(await amazonContext.ToListAsync());
        }

        // GET: AmazonMarketplacesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountMarketplaces = await _context.AccountMarketplaces.SingleOrDefaultAsync(m => m.MarketplaceID == id);
            if (accountMarketplaces == null)
            {
                return NotFound();
            }

            return View(accountMarketplaces);
        }

        // GET: AmazonMarketplacesController/Create
        public IActionResult Create()
        {
            ViewData["AccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "AmazonAccountID");
            ViewData["MarketplaceID"] = new SelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "AmazonMarketplaceID");
            return View();
        }

        // POST: AmazonMarketplacesController/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarketplaceID,AccountID")] AccountMarketplaces accountMarketplaces)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountMarketplaces);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "AmazonAccountID", accountMarketplaces.AccountID);
            ViewData["MarketplaceID"] = new SelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "AmazonMarketplaceID", accountMarketplaces.MarketplaceID);
            return View(accountMarketplaces);
        }

        // GET: AmazonMarketplacesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountMarketplaces = await _context.AccountMarketplaces.SingleOrDefaultAsync(m => m.MarketplaceID == id);
            if (accountMarketplaces == null)
            {
                return NotFound();
            }
            ViewData["AccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "AmazonAccountID", accountMarketplaces.AccountID);
            ViewData["MarketplaceID"] = new SelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "AmazonMarketplaceID", accountMarketplaces.MarketplaceID);
            return View(accountMarketplaces);
        }

        // POST: AmazonMarketplacesController/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarketplaceID,AccountID")] AccountMarketplaces accountMarketplaces)
        {
            if (id != accountMarketplaces.MarketplaceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountMarketplaces);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountMarketplacesExists(accountMarketplaces.MarketplaceID))
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
            ViewData["AccountID"] = new SelectList(_context.AmazonAccounts, "AmazonAccountID", "AmazonAccountID", accountMarketplaces.AccountID);
            ViewData["MarketplaceID"] = new SelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "AmazonMarketplaceID", accountMarketplaces.MarketplaceID);
            return View(accountMarketplaces);
        }

        // GET: AmazonMarketplacesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountMarketplaces = await _context.AccountMarketplaces.SingleOrDefaultAsync(m => m.MarketplaceID == id);
            if (accountMarketplaces == null)
            {
                return NotFound();
            }

            return View(accountMarketplaces);
        }

        // POST: AmazonMarketplacesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountMarketplaces = await _context.AccountMarketplaces.SingleOrDefaultAsync(m => m.MarketplaceID == id);
            _context.AccountMarketplaces.Remove(accountMarketplaces);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AccountMarketplacesExists(int id)
        {
            return _context.AccountMarketplaces.Any(e => e.MarketplaceID == id);
        }
    }
}
