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
    public class AmazonImageURLsController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonImageURLsController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonImageURLs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AmazonImageURLs.ToListAsync());
        }

        // GET: AmazonImageURLs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonImageURL = await _context.AmazonImageURLs.SingleOrDefaultAsync(m => m.AmazonImageURLID == id);
            if (amazonImageURL == null)
            {
                return NotFound();
            }

            return View(amazonImageURL);
        }

        // GET: AmazonImageURLs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmazonImageURLs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonImageURLID,ImageURL")] AmazonImageURL amazonImageURL)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonImageURL);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonImageURL);
        }

        // GET: AmazonImageURLs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonImageURL = await _context.AmazonImageURLs.SingleOrDefaultAsync(m => m.AmazonImageURLID == id);
            if (amazonImageURL == null)
            {
                return NotFound();
            }
            return View(amazonImageURL);
        }

        // POST: AmazonImageURLs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonImageURLID,ImageURL")] AmazonImageURL amazonImageURL)
        {
            if (id != amazonImageURL.AmazonImageURLID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonImageURL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonImageURLExists(amazonImageURL.AmazonImageURLID))
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
            return View(amazonImageURL);
        }

        // GET: AmazonImageURLs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonImageURL = await _context.AmazonImageURLs.SingleOrDefaultAsync(m => m.AmazonImageURLID == id);
            if (amazonImageURL == null)
            {
                return NotFound();
            }

            return View(amazonImageURL);
        }

        // POST: AmazonImageURLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonImageURL = await _context.AmazonImageURLs.SingleOrDefaultAsync(m => m.AmazonImageURLID == id);
            _context.AmazonImageURLs.Remove(amazonImageURL);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AmazonImageURLExists(int id)
        {
            return _context.AmazonImageURLs.Any(e => e.AmazonImageURLID == id);
        }
    }
}
