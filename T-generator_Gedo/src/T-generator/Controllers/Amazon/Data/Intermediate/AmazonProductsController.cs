using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Intermediate;

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

            var amazonProduct = await _context.AmazonProducts.SingleOrDefaultAsync(m => m.AmazonProductID == id);
            if (amazonProduct == null)
            {
                return NotFound();
            }

            return View(amazonProduct);
        }

        // GET: AmazonProducts/Create
        public IActionResult Create()
        {
            ViewData["AmazonTypeID"] = new SelectList(_context.AmazonTypes, "AmazonTypeID", "AmazonTypeID");
            return View();
        }

        // POST: AmazonProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonProductID,AmazonTypeID,Description,Name,Prefix")] AmazonProduct amazonProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AmazonTypeID"] = new SelectList(_context.AmazonTypes, "AmazonTypeID", "AmazonTypeID", amazonProduct.AmazonTypeID);
            return View(amazonProduct);
        }

        // GET: AmazonProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonProduct = await _context.AmazonProducts.SingleOrDefaultAsync(m => m.AmazonProductID == id);
            if (amazonProduct == null)
            {
                return NotFound();
            }
            ViewData["AmazonTypeID"] = new SelectList(_context.AmazonTypes, "AmazonTypeID", "AmazonTypeID", amazonProduct.AmazonTypeID);
            return View(amazonProduct);
        }

        // POST: AmazonProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonProductID,AmazonTypeID,Description,Name,Prefix")] AmazonProduct amazonProduct)
        {
            if (id != amazonProduct.AmazonProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonProductExists(amazonProduct.AmazonProductID))
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
            ViewData["AmazonTypeID"] = new SelectList(_context.AmazonTypes, "AmazonTypeID", "AmazonTypeID", amazonProduct.AmazonTypeID);
            return View(amazonProduct);
        }

        // GET: AmazonProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonProduct = await _context.AmazonProducts.SingleOrDefaultAsync(m => m.AmazonProductID == id);
            if (amazonProduct == null)
            {
                return NotFound();
            }

            return View(amazonProduct);
        }

        // POST: AmazonProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonProduct = await _context.AmazonProducts.SingleOrDefaultAsync(m => m.AmazonProductID == id);
            _context.AmazonProducts.Remove(amazonProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AmazonProductExists(int id)
        {
            return _context.AmazonProducts.Any(e => e.AmazonProductID == id);
        }
    }
}
