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
    public class AmazonListingsController : Controller
    {
        private const string COLOR_DIR = "uploads/Generated";

        private readonly AmazonContext _context;
        private IHostingEnvironment _environment;

        public AmazonListingsController(AmazonContext context, IHostingEnvironment environment)
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
            var items = from s in _context.AmazonListings
                        .Include(i => i.AmazonProduct)
                        .Include(i => i.AmazonProductColor)
                        .Include(i => i.AmazonDesign)
                        select s;
            return View(await PaginatedList<AmazonListing>.CreateAsync(items.AsNoTracking(), page ?? 1, itemsPerPage));
        }

        // GET: AmazonProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonListing = await _context.AmazonListings
                .Include(i=>i.AmazonProduct)
                .Include(i=>i.AmazonProductColor)
                .Include(i => i.AmazonDesign)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AmazonListingID == id);
            if (amazonListing == null)
            {
                return NotFound();
            }

            return View(amazonListing);
        }

        // GET: AmazonProducts/Create
        public IActionResult Create()
        {
            var amazonListing = new AmazonListing();
            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "Name");
            ViewData["AmazonProductColorID"] = new SelectList(_context.AmazonProductColors, "AmazonProductColorID", "Name");
            ViewData["AmazonDesignID"] = new SelectList(_context.AmazonDesigns, "AmazonDesignID", "Name");
            return View();
        }


        // POST: AmazonProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmazonProductColorID,AmazonProductID,DesignURL,AmazonDesignID,Top,Left,Right")] AmazonListing amazonListing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amazonListing);
                _context.SaveChanges();

                AmazonProductColor amazonProductColor = await _context.AmazonProductColors.SingleOrDefaultAsync(e => e.AmazonProductColorID == amazonListing.AmazonProductColorID);
                AmazonDesign amazonDesign = await _context.AmazonDesigns.SingleOrDefaultAsync(e => e.AmazonDesignID == amazonListing.AmazonDesignID);

                var baseImage = Path.Combine(_environment.WebRootPath,amazonProductColor.DesignURL);
                var opacity = amazonProductColor.Opacity;
                var coverImage = Path.Combine(_environment.WebRootPath, amazonDesign.DesignURL);
                var designURL = Path.Combine(COLOR_DIR, amazonListing.AmazonListingID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg");
                var outputImage = Path.Combine(_environment.WebRootPath, designURL);

                Point topLeft = new Point(amazonListing.Left, amazonListing.Top);
                Point topRight = new Point(amazonListing.Right, amazonListing.Top);

                using (FileStream savePic = System.IO.File.OpenWrite(outputImage))
                {
                    T_generator.Services.Amazon.ImageCollider.MergeImages(
                        new BackPicture(baseImage, topLeft, topRight),
                        new FrontPicture(coverImage, (int)(opacity*100)))
                            //.DrawLines(new Color(1, 1, 1), 1, new Vector2[] { new Vector2(topLeft.X, 0), new Vector2(topLeft.X, 1000) })
                            //.DrawLines(new Color(1, 1, 1), 1, new Vector2[] { new Vector2(0, topLeft.Y), new Vector2(1000, topLeft.Y) })
                            .Save(savePic);
                    amazonListing.DesignURL = designURL;
                }
                // _context.Add(amazonDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit",new { id = amazonListing.AmazonListingID });
            }
            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "Name");
            ViewData["AmazonProductColorID"] = new SelectList(_context.AmazonProductColors, "AmazonProductColorID", "Name");
            ViewData["AmazonDesignID"] = new SelectList(_context.AmazonDesigns, "AmazonDesignID", "Name");
            return View(amazonListing);
        }

        // GET: AmazonProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonListing = await _context.AmazonListings
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AmazonListingID == id);
            if (amazonListing == null)
            {
                return NotFound();
            }

            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "Name", amazonListing.AmazonProductID);
            ViewData["AmazonProductColorID"] = new SelectList(_context.AmazonProductColors, "AmazonProductColorID", "Name", amazonListing.AmazonProductColorID);
            ViewData["AmazonDesignID"] = new SelectList(_context.AmazonDesigns, "AmazonDesignID", "Name", amazonListing.AmazonDesignID);

            return View(amazonListing);
        }

        // POST: AmazonProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmazonListingID,AmazonProductColorID,AmazonProductID,DesignURL,AmazonDesignID,Top,Left,Right")] AmazonListing amazonListing)
        {
            if (id != amazonListing.AmazonListingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AmazonProductColor amazonProductColor = await _context.AmazonProductColors.SingleOrDefaultAsync(e => e.AmazonProductColorID == amazonListing.AmazonProductColorID);
                    AmazonDesign amazonDesign = await _context.AmazonDesigns.SingleOrDefaultAsync(e => e.AmazonDesignID == amazonListing.AmazonDesignID);

                    var baseImage = Path.Combine(_environment.WebRootPath, amazonProductColor.DesignURL);
                    var opacity = amazonProductColor.Opacity;
                    var coverImage = Path.Combine(_environment.WebRootPath, amazonDesign.DesignURL);
                    var designURL = Path.Combine(COLOR_DIR, amazonListing.AmazonListingID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg");
                    var outputImage = Path.Combine(_environment.WebRootPath, designURL);

                    Point topLeft = new Point(amazonListing.Left, amazonListing.Top);
                    Point topRight = new Point(amazonListing.Right, amazonListing.Top);

                    using (FileStream savePic = System.IO.File.OpenWrite(outputImage))
                    {
                        System.IO.File.Delete(Path.Combine(_environment.WebRootPath, amazonListing.DesignURL));
                        T_generator.Services.Amazon.ImageCollider.MergeImages(
                            new BackPicture(baseImage, topLeft, topRight),
                            new FrontPicture(coverImage, (int)(opacity * 100)))
                                //.DrawLines(new Color(1, 1, 1), 1, new Vector2[] { new Vector2(topLeft.X, 0), new Vector2(topLeft.X, 1000) })
                                //.DrawLines(new Color(1, 1, 1), 1, new Vector2[] { new Vector2(0, topLeft.Y), new Vector2(1000, topLeft.Y) })
                                .Save(savePic);
                        amazonListing.DesignURL = designURL;
                    }

                    //var fullUploadPath = Path.Combine(_environment.WebRootPath, COLOR_DIR);
                    //    var extension = DesignURL.FileName.Split('.').Last();
                    //    var filename = amazonListing.AmazonListingID + "." + extension;                                    
                    //    fullUploadPath = Path.Combine(fullUploadPath, filename);
                    //    using (var fileStream = new FileStream(fullUploadPath, FileMode.Create))
                    //    {
                    //        System.IO.File.Delete(Path.Combine(_environment.WebRootPath, oldURL));
                    //        await DesignURL.CopyToAsync(fileStream);
                    //        amazonListing.DesignURL = Path.Combine(COLOR_DIR, filename);
                    //    }
                    _context.Update(amazonListing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmazonListingExists(amazonListing.AmazonListingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", new { id = amazonListing.AmazonListingID });
            }

            amazonListing = await _context.AmazonListings
                .Include(i => i.AmazonProduct)
                .Include(i => i.AmazonProductColor)
                .Include(i => i.AmazonDesign)
                .SingleOrDefaultAsync(m => m.AmazonListingID == id);

            ViewData["AmazonProductID"] = new SelectList(_context.AmazonProducts, "AmazonProductID", "Name", amazonListing.AmazonProductID);
            ViewData["AmazonProductColorID"] = new SelectList(_context.AmazonProductColors, "AmazonProductColorID", "Name", amazonListing.AmazonProductColorID);
            ViewData["AmazonDesignID"] = new SelectList(_context.AmazonDesigns, "AmazonDesignID", "Name", amazonListing.AmazonDesignID);

            return View(amazonListing);
        }

        // GET: AmazonProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amazonListing = await _context.AmazonListings
                .Include(i => i.AmazonProduct)
                .Include(i => i.AmazonProductColor)
                .Include(i => i.AmazonDesign)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AmazonListingID == id);

            if (amazonListing == null)
            {
                return NotFound();
            }

            return View(amazonListing);
        }

        // POST: AmazonProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amazonListing = await _context.AmazonListings
                .Include(i => i.AmazonProduct)
                .Include(i => i.AmazonProductColor)
                .Include(i => i.AmazonDesign).SingleOrDefaultAsync(m => m.AmazonListingID == id);
            _context.AmazonListings.Remove(amazonListing);
            await _context.SaveChangesAsync();
            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, amazonListing.DesignURL));
            return RedirectToAction("Index");
        }
        

        private bool AmazonListingExists(int id)
        {
            return _context.AmazonListings.Any(e => e.AmazonListingID == id);
        }

    }
}
