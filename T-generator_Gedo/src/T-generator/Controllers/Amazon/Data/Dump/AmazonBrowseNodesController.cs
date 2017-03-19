using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Services.Amazon;

namespace T_generator.Controllers.Amazon.Data.Dump
{
    public class AmazonBrowseNodesController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonBrowseNodesController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonBrowseNodes
        public async Task<IActionResult> Index(int? page)
        {
            int itemsPerPage = 35;
            var items = from s in _context.AmazonBrowseNodes
                        select s;
            return View(await PaginatedList<AmazonBrowseNode>.CreateAsync(items.AsNoTracking(), page ?? 1, itemsPerPage));
        }

        // GET: AmazonBrowseNodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonBrowseNode = await _context.AmazonBrowseNodes.SingleOrDefaultAsync(m => m.AmazonBrowseNodeID == id);
            if (amazonBrowseNode == null)
            {
                return NotFound();
            }

            return View(amazonBrowseNode);
        }

        // GET: AmazonBrowseNodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmazonBrowseNodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonBrowseNodeID,BrowseNode")] AmazonBrowseNode amazonBrowseNode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonBrowseNode);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonBrowseNode);
        }

        // GET: AmazonBrowseNodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonBrowseNode = await _context.AmazonBrowseNodes.SingleOrDefaultAsync(m => m.AmazonBrowseNodeID == id);
            if (amazonBrowseNode == null)
            {
                return NotFound();
            }
            return View(amazonBrowseNode);
        }

        // POST: AmazonBrowseNodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonBrowseNodeID,BrowseNode")] AmazonBrowseNode amazonBrowseNode)
        {
            if (id != amazonBrowseNode.AmazonBrowseNodeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonBrowseNode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonBrowseNodeExists(amazonBrowseNode.AmazonBrowseNodeID))
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
            return View(amazonBrowseNode);
        }

        // GET: AmazonBrowseNodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonBrowseNode = await _context.AmazonBrowseNodes.SingleOrDefaultAsync(m => m.AmazonBrowseNodeID == id);
            if (amazonBrowseNode == null)
            {
                return NotFound();
            }

            return View(amazonBrowseNode);
        }

        // POST: AmazonBrowseNodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonBrowseNode = await _context.AmazonBrowseNodes.SingleOrDefaultAsync(m => m.AmazonBrowseNodeID == id);
            _context.AmazonBrowseNodes.Remove(amazonBrowseNode);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AmazonBrowseNodeExists(int id)
        {
            return _context.AmazonBrowseNodes.Any(e => e.AmazonBrowseNodeID == id);
        }
    }
}
