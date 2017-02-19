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
    public class AmazonAccountsController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonAccountsController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.AmazonAccounts.ToListAsync());
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
            return View();
        }

        // POST: AmazonAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonAccountID,Name,Prefix")] AmazonAccount amazonAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonAccount);
        }

        // GET: AmazonAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: AmazonAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonAccountID,Name,Prefix")] AmazonAccount amazonAccount)
        {
            if (id != amazonAccount.AmazonAccountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonAccountExists(amazonAccount.AmazonAccountID))
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
            
            return View(amazonAccount);
        }

        // GET: AmazonAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
    }
}
