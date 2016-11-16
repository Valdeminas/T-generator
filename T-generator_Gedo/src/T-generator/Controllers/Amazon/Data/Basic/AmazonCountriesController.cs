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
    public class AmazonCountriesController : Controller
    {
        private readonly AmazonContext _context;

        public AmazonCountriesController(AmazonContext context)
        {
            _context = context;    
        }

        // GET: AmazonCountries
        public async Task<IActionResult> Index()
        {
            return View(await _context.AmazonCountries.ToListAsync());
        }

        // GET: AmazonCountries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonCountry = await _context.AmazonCountries.SingleOrDefaultAsync(m => m.AmazonCountryID == id);
            if (amazonCountry == null)
            {
                return NotFound();
            }

            return View(amazonCountry);
        }

        // GET: AmazonCountries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmazonCountries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonCountryID,Name,Prefix")] AmazonCountry amazonCountry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonCountry);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonCountry);
        }

        // GET: AmazonCountries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonCountry = await _context.AmazonCountries.SingleOrDefaultAsync(m => m.AmazonCountryID == id);
            if (amazonCountry == null)
            {
                return NotFound();
            }
            return View(amazonCountry);
        }

        // POST: AmazonCountries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonCountryID,Name,Prefix")] AmazonCountry amazonCountry)
        {
            if (id != amazonCountry.AmazonCountryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amazonCountry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonCountryExists(amazonCountry.AmazonCountryID))
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
            return View(amazonCountry);
        }

        // GET: AmazonCountries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonCountry = await _context.AmazonCountries.SingleOrDefaultAsync(m => m.AmazonCountryID == id);
            if (amazonCountry == null)
            {
                return NotFound();
            }

            return View(amazonCountry);
        }

        // POST: AmazonCountries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonCountry = await _context.AmazonCountries.SingleOrDefaultAsync(m => m.AmazonCountryID == id);
            _context.AmazonCountries.Remove(amazonCountry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AmazonCountryExists(int id)
        {
            return _context.AmazonCountries.Any(e => e.AmazonCountryID == id);
        }
    }
}
