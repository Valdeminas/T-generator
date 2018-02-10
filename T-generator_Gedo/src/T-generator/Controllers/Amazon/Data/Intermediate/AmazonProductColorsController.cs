using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T_generator.Data;
using T_generator.Models.Amazon.Data.Intermediate;
using T_generator.AmazonViewModel;
using T_generator.Models.Amazon.Data.Dump;
using Microsoft.CodeAnalysis;
using T_generator.Models.Amazon.Data.JoinTables;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Services.Amazon;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ImageSharp;
using System.Numerics;

namespace T_generator.Controllers.Amazon.Data.Intermediate
{
    public class AmazonProductColorsController : Controller
    {
        private const string THUMB_DIR = "uploads/Thumbs";
        private const string COLOR_DIR = "uploads/Colors";

        private readonly AmazonContext _context;
        private IHostingEnvironment _environment;

        public AmazonProductColorsController(AmazonContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AmazonProducts
        public async Task<IActionResult> Index(int? page)
        {
            //var amazonContext = _context.AmazonProducts.Include(a => a.AmazonType);
            //return View(await amazonContext.ToListAsync());

            int itemsPerPage = 35;
            var items = from s in _context.AmazonProductColors.Include(a => a.AmazonProduct)
                        select s;
            return View(await PaginatedList<AmazonProductColor>.CreateAsync(items.AsNoTracking(), page ?? 1, itemsPerPage));
        }

        // GET: AmazonProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonProductColor = await _context.AmazonProductColors
                .Include(i=>i.AmazonProduct)
                .Include(i=>i.AmazonColor)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AmazonProductColorID == id);
            if (amazonProductColor == null)
            {
                return NotFound();
            }

            return View(amazonProductColor);
        }

        // GET: AmazonProducts/Create
        public IActionResult Create()
        {
            var amazonProductColor = new AmazonProductColor();
            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "Name");
            ViewData["AmazonColorID"] = new SelectList(_context.AmazonColors, "AmazonColorID", "Name");
            return View();
        }


        // POST: AmazonProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonProductColorID,AmazonProductID,AmazonColorID,DesignURL,Name,Opacity,Top,Left,Right,Bot")] AmazonProductColor amazonProductColor, IFormFile DesignURL)
        {
            if (ModelState.IsValid && DesignURL != null)
            {
                _context.Add(amazonProductColor);
                _context.SaveChanges();
                var fullUploadPath = Path.Combine(_environment.WebRootPath, COLOR_DIR);
                var extension = DesignURL.FileName.Split('.').Last();
                var filename = amazonProductColor.AmazonProductColorID + "." + extension;
                fullUploadPath = Path.Combine(fullUploadPath, filename);

                using (var fileStream = new FileStream(fullUploadPath, FileMode.Create))
                {
                    await DesignURL.CopyToAsync(fileStream);
                    amazonProductColor.DesignURL = Path.Combine(COLOR_DIR, filename);
                }

                var thumbURL = Path.Combine(THUMB_DIR, amazonProductColor.AmazonProductColorID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg");
                var outputImage = Path.Combine(_environment.WebRootPath, thumbURL);
                var baseImage = Path.Combine(_environment.WebRootPath, amazonProductColor.DesignURL);

                System.Drawing.Image m_baseimage= System.Drawing.Image.FromFile(baseImage); ;
                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black);

                System.Drawing.Graphics.FromImage(m_baseimage).DrawLine(myPen, new System.Drawing.Point(amazonProductColor.Left, 0), new System.Drawing.Point(amazonProductColor.Left, m_baseimage.Height));
                System.Drawing.Graphics.FromImage(m_baseimage).DrawLine(myPen, new System.Drawing.Point(amazonProductColor.Right, 0), new System.Drawing.Point(amazonProductColor.Right, m_baseimage.Height));
                System.Drawing.Graphics.FromImage(m_baseimage).DrawLine(myPen, new System.Drawing.Point(0, amazonProductColor.Top), new System.Drawing.Point(m_baseimage.Width, amazonProductColor.Top));
                System.Drawing.Graphics.FromImage(m_baseimage).DrawLine(myPen, new System.Drawing.Point(0, amazonProductColor.Bot), new System.Drawing.Point(m_baseimage.Width, amazonProductColor.Bot));

                m_baseimage.Save(outputImage);
                m_baseimage.Dispose();

                amazonProductColor.ThumbURL = thumbURL;

                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = amazonProductColor.AmazonProductColorID });
            }
            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "Name");
            ViewData["AmazonColorID"] = new SelectList(_context.AmazonColors, "AmazonColorID", "Name");
            return View(amazonProductColor);
        }

        // GET: AmazonProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonProductColor = await _context.AmazonProductColors
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AmazonProductColorID == id);
            if (amazonProductColor == null)
            {
                return NotFound();
            }
            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "Name", amazonProductColor.AmazonProductID);
            ViewData["AmazonColorID"] = new SelectList(_context.AmazonColors, "AmazonColorID", "Name", amazonProductColor.AmazonColorID);
            return View(amazonProductColor);
        }

        // POST: AmazonProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonProductColorID,AmazonProductID,AmazonColorID,DesignURL,Name,Opacity,Top,Left,Right,Bot")] AmazonProductColor amazonProductColor, IFormFile DesignURL, string oldURL,string oldThumb)
        {
            if (id != amazonProductColor.AmazonProductColorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (DesignURL != null)
                    {
                        var fullUploadPath = Path.Combine(_environment.WebRootPath, COLOR_DIR);
                        var extension = DesignURL.FileName.Split('.').Last();
                        var filename = amazonProductColor.AmazonProductColorID + "." + extension;
                        fullUploadPath = Path.Combine(fullUploadPath, filename);
                        using (var fileStream = new FileStream(fullUploadPath, FileMode.Create))
                        {
                            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, oldURL));
                            await DesignURL.CopyToAsync(fileStream);
                            amazonProductColor.DesignURL = Path.Combine(COLOR_DIR, filename);
                        }
                    }
                    else
                    {
                        amazonProductColor.DesignURL = oldURL;
                    }

                    var thumbURL = Path.Combine(THUMB_DIR, amazonProductColor.AmazonProductColorID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg");
                    var outputImage = Path.Combine(_environment.WebRootPath, thumbURL);
                    var baseImage = Path.Combine(_environment.WebRootPath, amazonProductColor.DesignURL);

                    System.IO.File.Delete(Path.Combine(_environment.WebRootPath, oldThumb));

                    System.Drawing.Image m_baseimage = System.Drawing.Image.FromFile(baseImage);
                    System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black);

                    System.Drawing.Graphics.FromImage(m_baseimage).DrawLine(myPen, new System.Drawing.Point(amazonProductColor.Left, 0), new System.Drawing.Point(amazonProductColor.Left, m_baseimage.Height));
                    System.Drawing.Graphics.FromImage(m_baseimage).DrawLine(myPen, new System.Drawing.Point(amazonProductColor.Right, 0), new System.Drawing.Point(amazonProductColor.Right, m_baseimage.Height));
                    System.Drawing.Graphics.FromImage(m_baseimage).DrawLine(myPen, new System.Drawing.Point(0, amazonProductColor.Top), new System.Drawing.Point(m_baseimage.Width, amazonProductColor.Top));
                    System.Drawing.Graphics.FromImage(m_baseimage).DrawLine(myPen, new System.Drawing.Point(0, amazonProductColor.Bot), new System.Drawing.Point(m_baseimage.Width, amazonProductColor.Bot));

                    m_baseimage.Save(outputImage);
                    m_baseimage.Dispose();

                    amazonProductColor.ThumbURL = thumbURL;

                        _context.Update(amazonProductColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonProductColorExists(amazonProductColor.AmazonProductColorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", new { id = amazonProductColor.AmazonProductColorID });
            }

            amazonProductColor = await _context.AmazonProductColors
                .Include(i => i.AmazonProduct)
                .Include(i=>i.AmazonColor)
                .SingleOrDefaultAsync(m => m.AmazonProductColorID == id);

            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "Name", amazonProductColor.AmazonProductID);
            ViewData["AmazonColorID"] = new SelectList(_context.AmazonColors, "AmazonColorID", "Name", amazonProductColor.AmazonColorID);
            return View(amazonProductColor);
        }

        // GET: AmazonProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonProductColor = await _context.AmazonProductColors
                .Include(i => i.AmazonProduct)
                .Include(i=>i.AmazonColor)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AmazonProductColorID == id);
            if (amazonProductColor == null)
            {
                return NotFound();
            }

            return View(amazonProductColor);
        }

        // POST: AmazonProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonProductColor = await _context.AmazonProductColors.Include(i => i.AmazonProduct).Include(i=>i.AmazonColor).SingleOrDefaultAsync(m => m.AmazonProductColorID == id);
            _context.AmazonProductColors.Remove(amazonProductColor);
            await _context.SaveChangesAsync();
            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, amazonProductColor.DesignURL));
            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, amazonProductColor.ThumbURL));
            return RedirectToAction("Index");
        }
        

        private bool AmazonProductColorExists(int id)
        {
            return _context.AmazonProductColors.Any(e => e.AmazonProductColorID == id);
        }

    }
}
