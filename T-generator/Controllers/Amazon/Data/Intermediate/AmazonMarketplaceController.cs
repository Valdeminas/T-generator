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
using System.Data.Entity.Infrastructure;

namespace T_generator.Controllers.Amazon.Data.Intermediate
{
    public class AmazonMarketplaceController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonMarketplace/
        public ActionResult Index()
        {
            return View(db.AmazonMarketplaces.ToList());
        }

        // GET: /AmazonMarketplace/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonMarketplace amazonmarketplace = db.AmazonMarketplaces.Find(id);
            if (amazonmarketplace == null)
            {
                return HttpNotFound();
            }
            return View(amazonmarketplace);
        }

        // GET: /AmazonMarketplace/Create
        public ActionResult Create()
        {
            PopulateCurrenciesDropDownList();
            return View();
        }

        // POST: /AmazonMarketplace/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonMarketplaceID,Name,Prefix,AmazonCurrencyID")] AmazonMarketplace amazonmarketplace)
        {
            if (ModelState.IsValid)
            {
                db.AmazonMarketplaces.Add(amazonmarketplace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            PopulateCurrenciesDropDownList(amazonmarketplace.AmazonCurrencyID);
            return View(amazonmarketplace);
        }

        // GET: /AmazonMarketplace/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonMarketplace amazonmarketplace = db.AmazonMarketplaces.Find(id);
            if (amazonmarketplace == null)
            {
                return HttpNotFound();
            }

            PopulateCurrenciesDropDownList(amazonmarketplace.AmazonCurrencyID);
            return View(amazonmarketplace);
        }

        // POST: /AmazonMarketplace/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var marketplaceToUpdate = db.AmazonMarketplaces.Find(id);
            if (TryUpdateModel(marketplaceToUpdate, "",
               new string[] { "Name", "Prefix", "AmazonCurrencyID" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException  /*dex*/ )
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateCurrenciesDropDownList(marketplaceToUpdate.AmazonCurrencyID);
            return View(marketplaceToUpdate);
        }

        // GET: /AmazonMarketplace/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonMarketplace amazonmarketplace = db.AmazonMarketplaces.Find(id);
            if (amazonmarketplace == null)
            {
                return HttpNotFound();
            }
            return View(amazonmarketplace);
        }

        // POST: /AmazonMarketplace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonMarketplace amazonmarketplace = db.AmazonMarketplaces.Find(id);
            db.AmazonMarketplaces.Remove(amazonmarketplace);
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

        private void PopulateCurrenciesDropDownList(object selectedCurrencyID = null)
        {
            var currenciesQuery = from d in db.AmazonCurrencies
                                   orderby d.Currency
                                   select d;
            ViewBag.AmazonCurrencyID = new SelectList(currenciesQuery, "AmazonCurrencyID", "Currency", selectedCurrencyID);
        } 
    }
}
