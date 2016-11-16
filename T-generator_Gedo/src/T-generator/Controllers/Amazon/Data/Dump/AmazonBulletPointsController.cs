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
    public class AmazonBulletPointsController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonBulletPointsController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonBulletPoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.AmazonBulletPoints.ToListAsync());
        }

        // GET: AmazonBulletPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonBulletPoint = await _context.AmazonBulletPoints.SingleOrDefaultAsync(m => m.AmazonBulletPointID == id);
            if (amazonBulletPoint == null)
            {
                return NotFound();
            }

            return View(amazonBulletPoint);
        }

        // GET: AmazonBulletPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmazonBulletPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonBulletPointID,BulletPoint")] AmazonBulletPoint amazonBulletPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonBulletPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonBulletPoint);
        }

        // GET: AmazonBulletPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonBulletPoint = await _context.AmazonBulletPoints.SingleOrDefaultAsync(m => m.AmazonBulletPointID == id);
            if (amazonBulletPoint == null)
            {
                return NotFound();
            }
            return View(amazonBulletPoint);
        }

        // POST: AmazonBulletPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonBulletPointID,BulletPoint")] AmazonBulletPoint amazonBulletPoint)
        {
            if (id != amazonBulletPoint.AmazonBulletPointID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonBulletPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonBulletPointExists(amazonBulletPoint.AmazonBulletPointID))
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
            return View(amazonBulletPoint);
        }

        // GET: AmazonBulletPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonBulletPoint = await _context.AmazonBulletPoints.SingleOrDefaultAsync(m => m.AmazonBulletPointID == id);
            if (amazonBulletPoint == null)
            {
                return NotFound();
            }

            return View(amazonBulletPoint);
        }

        // POST: AmazonBulletPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonBulletPoint = await _context.AmazonBulletPoints.SingleOrDefaultAsync(m => m.AmazonBulletPointID == id);
            _context.AmazonBulletPoints.Remove(amazonBulletPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AmazonBulletPointExists(int id)
        {
            return _context.AmazonBulletPoints.Any(e => e.AmazonBulletPointID == id);
        }
    }
}
