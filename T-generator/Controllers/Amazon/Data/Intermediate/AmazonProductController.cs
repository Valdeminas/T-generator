using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using T_generator.Models;
using T_generator.DAL;
using T_generator.ViewModel;
using System.Data.Entity.Infrastructure;
using T_generator.Models.Amazon.Data.Dump;

namespace T_generator.Controllers.Amazon.Data.Intermediate
{
    public class AmazonProductController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonProduct/
        public ActionResult Index()
        {
            return View(db.AmazonProducts.ToList());
        }

        // GET: /AmazonProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonProduct amazonproduct = db.AmazonProducts.Find(id);
            if (amazonproduct == null)
            {
                return HttpNotFound();
            }
            return View(amazonproduct);
        }

        // GET: /AmazonProduct/Create
        public ActionResult Create()
        {
            PopulateTypeDropDownList();
            PopulateAssignedKeywordData();
            return View();
        }

        // POST: /AmazonProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AmazonProductID,Name,Prefix,Description,AmazonTypeID,Keywords")] AmazonProduct amazonproduct, string[] selectedKeywords)
        {
            if (ModelState.IsValid)
            {
                amazonproduct.Keywords=new List<AmazonKeyword>();
                UpdateProductKeywords(selectedKeywords, amazonproduct);
                db.AmazonProducts.Add(amazonproduct);               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateTypeDropDownList(amazonproduct.AmazonTypeID);
            PopulateAssignedKeywordData(amazonproduct);
            return View(amazonproduct);
        }

        // GET: /AmazonProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonProduct amazonproduct = db.AmazonProducts
                .Include(i=>i.Keywords)
                .Where(i=>i.AmazonProductID == id)
                .Single();           
            if (amazonproduct == null)
            {
                return HttpNotFound();
            }

            PopulateAssignedKeywordData(amazonproduct);
            PopulateTypeDropDownList(amazonproduct.AmazonTypeID);
            return View(amazonproduct);
        }        

        // POST: /AmazonProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedKeywords)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productToUpdate = db.AmazonProducts
               .Include(i => i.Keywords)
               .Where(i => i.AmazonProductID == id)
               .Single();

            if (TryUpdateModel(productToUpdate, "",
               new string[] { "Name", "Prefix", "Description", "AmazonTypeID"}))
            {
                try
                {
                    UpdateProductKeywords(selectedKeywords, productToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateTypeDropDownList(productToUpdate.AmazonTypeID);
            PopulateAssignedKeywordData(productToUpdate);
            return View(productToUpdate);
        }       

        // GET: /AmazonProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonProduct amazonproduct = db.AmazonProducts.Find(id);
            if (amazonproduct == null)
            {
                return HttpNotFound();
            }
            return View(amazonproduct);
        }

        // POST: /AmazonProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonProduct amazonproduct = db.AmazonProducts.Find(id);
            db.AmazonProducts.Remove(amazonproduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateTypeDropDownList(object selectedTypeID = null)
        {
            var typesQuery = from d in db.AmazonTypes
                                  orderby d.Name
                                  select d;
            ViewBag.AmazonTypeID = new SelectList(typesQuery, "AmazonTypeID", "Name", selectedTypeID);
        }

        private void PopulateAssignedKeywordData(AmazonProduct amazonproduct = null)
        {
            var productKeywords = new HashSet<int>();
            if (amazonproduct != null && amazonproduct.Keywords != null)
            {
                productKeywords = new HashSet<int>(amazonproduct.Keywords.Select(c => c.AmazonKeywordID));
            }

            var allKeywords = db.AmazonKeywords;
            var viewModel = new List<AssignedKeywordsData>();
            foreach (var keyword in allKeywords)
            {
                viewModel.Add(new AssignedKeywordsData
                {
                    AmazonKeywordID = keyword.AmazonKeywordID,
                    Keyword = keyword.Keyword,
                    Assigned = productKeywords.Contains(keyword.AmazonKeywordID)
                });
            }
            ViewBag.Keywords = viewModel;
        }

        private void UpdateProductKeywords(string[] selectedKeywords, AmazonProduct productToUpdate)
        {
            if (selectedKeywords == null)
            {
                productToUpdate.Keywords = new List<AmazonKeyword>();
                return;
            }

            var selectedKeywordsHS = new HashSet<string>(selectedKeywords);
            var productKeywords = new HashSet<int>(productToUpdate.Keywords.Select(c => c.AmazonKeywordID));
            foreach (var keyword in db.AmazonKeywords)
            {
                if (selectedKeywordsHS.Contains(keyword.AmazonKeywordID.ToString()))
                {
                    if (!productKeywords.Contains(keyword.AmazonKeywordID))
                    {
                        productToUpdate.Keywords.Add(keyword);
                    }
                }
                else
                {
                    if (productKeywords.Contains(keyword.AmazonKeywordID))
                    {
                        productToUpdate.Keywords.Remove(keyword);
                    }
                }
            }
        }
    }
}
