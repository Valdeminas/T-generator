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

namespace T_generator.Controllers.Amazon.Data.Basic
{
    public class AmazonTypesController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonTypesController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonTypes
        public async Task<IActionResult> Index(int? page)
        {
            int itemsPerPage = 35;
            var items = from s in _context.AmazonTypes
                        select s;
            return View(await PaginatedList<AmazonType>.CreateAsync(items.AsNoTracking(), page ?? 1, itemsPerPage));
        }

        // GET: AmazonTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonType = await _context.AmazonTypes.SingleOrDefaultAsync(m => m.AmazonTypeID == id);
            if (amazonType == null)
            {
                return NotFound();
            }

            return View(amazonType);
        }

        // GET: AmazonTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmazonTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonTypeID,Name,Prefix")] AmazonType amazonType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonType);
        }

        // GET: AmazonTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonType = await _context.AmazonTypes.SingleOrDefaultAsync(m => m.AmazonTypeID == id);
            if (amazonType == null)
            {
                return NotFound();
            }
            return View(amazonType);
        }

        // POST: AmazonTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonTypeID,Name,Prefix")] AmazonType amazonType)
        {
            if (id != amazonType.AmazonTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonTypeExists(amazonType.AmazonTypeID))
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
            return View(amazonType);
        }

        // GET: AmazonTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonType = await _context.AmazonTypes.SingleOrDefaultAsync(m => m.AmazonTypeID == id);
            if (amazonType == null)
            {
                return NotFound();
            }

            return View(amazonType);
        }

        // POST: AmazonTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonType = await _context.AmazonTypes.SingleOrDefaultAsync(m => m.AmazonTypeID == id);
            if (!RelatedProductExists(id))
            {
                _context.AmazonTypes.Remove(amazonType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["RelatedEntriesExistError"] = "This object has other entries using it."
                    + Environment.NewLine
                    + "Delete the entries using this object first, and then try to delete this object.";
                return View(amazonType);
            }
        }

        private bool AmazonTypeExists(int id)
        {
            return _context.AmazonTypes.Any(e => e.AmazonTypeID == id);
        }

        private bool RelatedProductExists(int id)
        {
            return _context.AmazonProducts.Any(e => e.AmazonTypeID == id);
        }
    }
}
