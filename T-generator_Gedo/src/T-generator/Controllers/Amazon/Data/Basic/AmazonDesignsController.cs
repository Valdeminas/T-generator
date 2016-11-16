using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Basic;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;

namespace T_generator.Controllers.Amazon.Data.Basic
{
    public class AmazonDesignsController : Controller
    {
        private readonly AmazonContext _context;
        private IHostingEnvironment _environment;

        public AmazonDesignsController(AmazonContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AmazonDesigns
        public async Task<IActionResult> Index()
        {
            return View(await _context.AmazonDesigns.ToListAsync());
        }

        // GET: AmazonDesigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonDesign = await _context.AmazonDesigns.SingleOrDefaultAsync(m => m.AmazonDesignID == id);
            if (amazonDesign == null)
            {
                return NotFound();
            }

            return View(amazonDesign);
        }

        // GET: AmazonDesigns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AmazonDesigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonDesignID,DesignURL,Name,Prefix")] AmazonDesign amazonDesign,IFormFile DesignURL)
        {           
            if (ModelState.IsValid)
            {                            
                var uploadDir = "Designs";
                var fullUploadPath = Path.Combine(_environment.WebRootPath, uploadDir);
                var extension = DesignURL.FileName.Split('.').Last();
                var filename = amazonDesign.AmazonDesignID + "." + extension;
                fullUploadPath = Path.Combine(fullUploadPath, filename);
                if (DesignURL.Length > 0)
                {
                    using (var fileStream = new FileStream(fullUploadPath, FileMode.Create))
                    {
                        await DesignURL.CopyToAsync(fileStream);
                        amazonDesign.DesignURL = Path.Combine(uploadDir, filename);
                    }

                }
                _context.Add(amazonDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(amazonDesign);
        }

        // GET: AmazonDesigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonDesign = await _context.AmazonDesigns.SingleOrDefaultAsync(m => m.AmazonDesignID == id);
            if (amazonDesign == null)
            {
                return NotFound();
            }
            return View(amazonDesign);
        }

        // POST: AmazonDesigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonDesignID,DesignURL,Name,Prefix")] AmazonDesign amazonDesign, IFormFile DesignURL,string oldURL)
        {
            if (id != amazonDesign.AmazonDesignID)
            {
                return NotFound();
            }                       
            if (ModelState.IsValid)
            {
                try
                {
                    var uploadDir = "Designs";
                    var fullUploadPath = Path.Combine(_environment.WebRootPath, uploadDir);
                    var extension = DesignURL.FileName.Split('.').Last();
                    var filename = amazonDesign.AmazonDesignID + "." + extension;
                    fullUploadPath = Path.Combine(fullUploadPath, filename);
                    if (DesignURL.Length > 0)
                    {
                        using (var fileStream = new FileStream(fullUploadPath, FileMode.Create))
                        {
                            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, oldURL));
                            await DesignURL.CopyToAsync(fileStream);
                            amazonDesign.DesignURL = Path.Combine(uploadDir, filename);
                        }

                    }
                    _context.Update(amazonDesign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonDesignExists(amazonDesign.AmazonDesignID))
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
            return View(amazonDesign);
        }

        // GET: AmazonDesigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonDesign = await _context.AmazonDesigns.SingleOrDefaultAsync(m => m.AmazonDesignID == id);
            if (amazonDesign == null)
            {
                return NotFound();
            }

            return View(amazonDesign);
        }

        // POST: AmazonDesigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonDesign = await _context.AmazonDesigns.SingleOrDefaultAsync(m => m.AmazonDesignID == id);
            _context.AmazonDesigns.Remove(amazonDesign);
            await _context.SaveChangesAsync();
            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, amazonDesign.DesignURL));
            return RedirectToAction("Index");
        }

        private bool AmazonDesignExists(int id)
        {
            return _context.AmazonDesigns.Any(e => e.AmazonDesignID == id);
        }
    }
}
