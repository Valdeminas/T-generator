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
    public class AmazonSizesController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonSizesController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonSizes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AmazonSizes.ToListAsync());
        }

        // GET: AmazonSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonSize = await _context.AmazonSizes.SingleOrDefaultAsync(m => m.AmazonSizeID == id);
            if (amazonSize == null)
            {
                return NotFound();
            }

            return View(amazonSize);
        }

        // GET: AmazonSizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmazonSizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonSizeID,Name,Prefix")] AmazonSize amazonSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonSize);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonSize);
        }

        // GET: AmazonSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonSize = await _context.AmazonSizes.SingleOrDefaultAsync(m => m.AmazonSizeID == id);
            if (amazonSize == null)
            {
                return NotFound();
            }
            return View(amazonSize);
        }

        // POST: AmazonSizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonSizeID,Name,Prefix")] AmazonSize amazonSize)
        {
            if (id != amazonSize.AmazonSizeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonSizeExists(amazonSize.AmazonSizeID))
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
            return View(amazonSize);
        }

        // GET: AmazonSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonSize = await _context.AmazonSizes.SingleOrDefaultAsync(m => m.AmazonSizeID == id);
            if (amazonSize == null)
            {
                return NotFound();
            }

            return View(amazonSize);
        }

        // POST: AmazonSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonSize = await _context.AmazonSizes.SingleOrDefaultAsync(m => m.AmazonSizeID == id);
            if (!RelatedProductExists(id))
            {
                _context.AmazonSizes.Remove(amazonSize);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["RelatedEntriesExistError"] = "This object has other entries using it."
                    + Environment.NewLine
                    + "Delete the entries using this object first, and then try to delete this object.";
                return View(amazonSize);
            }
        }

        private bool AmazonSizeExists(int id)
        {
            return _context.AmazonSizes.Any(e => e.AmazonSizeID == id);
        }

        private bool RelatedProductExists(int id)
        {
            return _context.ProductSizes.Any(e => e.SizeID == id);
        }
    }
}
