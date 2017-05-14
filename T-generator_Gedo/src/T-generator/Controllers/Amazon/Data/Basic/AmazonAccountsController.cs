using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Services.Amazon;
using T_generator.Models.Amazon.Data.JoinTables;

namespace T_generator.Controllers.Amazon.Data.Basic
{
    public class AmazonAccountsController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonAccountsController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonAccounts
        public async Task<IActionResult> Index(int? page)
        {
            int itemsPerPage = 35;
            var items = from s in _context.AmazonAccounts
                        select s;
            return View(await PaginatedList<AmazonAccount>.CreateAsync(items.AsNoTracking(), page ?? 1, itemsPerPage));
        }

        // GET: AmazonAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonAccount = await _context.AmazonAccounts.SingleOrDefaultAsync(m => m.AmazonAccountID == id);
            if (amazonAccount == null)
            {
                return NotFound();
            }

            return View(amazonAccount);
        }

        // GET: AmazonAccounts/Create
        public IActionResult Create()
        {
            ViewData["AmazonMarketplaceID"] = new MultiSelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "Name");
            return View();
        }

        // POST: AmazonAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonAccountID,Name,Prefix")] AmazonAccount amazonAccount, List<int> Marketplaces)
        {
            amazonAccount.Marketplaces = new List<AccountMarketplaces>();
            UpdateAccountMarketplaces(Marketplaces, amazonAccount);
            if (ModelState.IsValid)
            {
                _context.Add(amazonAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AmazonMarketplaceID"] = new MultiSelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "Name", amazonAccount.Marketplaces.Select(i => i.MarketplaceID).ToArray());
            return View(amazonAccount);
        }

        // GET: AmazonAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonAccount = await _context.AmazonAccounts
                .Include(i=>i.Marketplaces)
                .SingleOrDefaultAsync(m => m.AmazonAccountID == id);
            if (amazonAccount == null)
            {
                return NotFound();
            }
            ViewData["AmazonMarketplaceID"] = new MultiSelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "Name", amazonAccount.Marketplaces.Select(i => i.MarketplaceID).ToArray());
            return View(amazonAccount);
        }

        // POST: AmazonAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonAccountID,Name,Prefix")] AmazonAccount amazonAccount, List<int> Marketplaces)
        {
            if (id != amazonAccount.AmazonAccountID)
            {
                return NotFound();
            }

            var productToUpdate = await _context.AmazonAccounts
                .Include(i => i.Marketplaces)
                .SingleOrDefaultAsync(m => m.AmazonAccountID == id);

            if (await TryUpdateModelAsync<AmazonAccount>(
                    productToUpdate,
                    "",
                    i => i.Name, i => i.Prefix))
            {
                UpdateAccountMarketplaces(Marketplaces, productToUpdate);
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
            ViewData["AmazonMarketplaceID"] = new MultiSelectList(_context.AmazonMarketplaces, "AmazonMarketplaceID", "Name", amazonAccount.Marketplaces.Select(i => i.MarketplaceID).ToArray());
            return View(amazonAccount);
        }

        // GET: AmazonAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonAccount = await _context.AmazonAccounts
                .Include(i=>i.Marketplaces)
                .SingleOrDefaultAsync(m => m.AmazonAccountID == id);
            if (amazonAccount == null)
            {
                return NotFound();
            }

            return View(amazonAccount);
        }

        // POST: AmazonAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonAccount = await _context.AmazonAccounts.SingleOrDefaultAsync(m => m.AmazonAccountID == id);
            _context.AmazonAccounts.Remove(amazonAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AmazonAccountExists(int id)
        {
            return _context.AmazonAccounts.Any(e => e.AmazonAccountID == id);
        }

        private void UpdateAccountMarketplaces(List<int> Marketplaces, AmazonAccount productToUpdate)
        {
            foreach (var marketplaceid in Marketplaces)
            {
                productToUpdate.Marketplaces.Add(new AccountMarketplaces { AccountID = productToUpdate.AmazonAccountID, MarketplaceID = marketplaceid });
            }
        }
    }
}
