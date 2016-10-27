using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using T_generator.Models.Amazon.Basic;
using T_generator.DAL;
using T_generator.Models;

namespace T_generator.Controllers.Amazon.Data.Basic
{
    public class AmazonCurrencyController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonCurrency/
        public ActionResult Index()
        {
            return View(db.AmazonCurrencies.ToList());
        }

        // GET: /AmazonCurrency/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonCurrency amazoncurrency = db.AmazonCurrencies.Find(id);
            if (amazoncurrency == null)
            {
                return HttpNotFound();
            }
            return View(amazoncurrency);
        }

        // GET: /AmazonCurrency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonCurrency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonCurrencyID,Currency")] AmazonCurrency amazoncurrency)
        {
            if (ModelState.IsValid)
            {
                db.AmazonCurrencies.Add(amazoncurrency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazoncurrency);
        }

        // GET: /AmazonCurrency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonCurrency amazoncurrency = db.AmazonCurrencies.Find(id);
            if (amazoncurrency == null)
            {
                return HttpNotFound();
            }
            return View(amazoncurrency);
        }

        // POST: /AmazonCurrency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonCurrencyID,Currency")] AmazonCurrency amazoncurrency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazoncurrency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazoncurrency);
        }

        // GET: /AmazonCurrency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonCurrency amazoncurrency = db.AmazonCurrencies.Find(id);
            if (amazoncurrency == null)
            {
                return HttpNotFound();
            }
            return View(amazoncurrency);
        }

        // POST: /AmazonCurrency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonCurrency amazoncurrency = db.AmazonCurrencies.Find(id); 

            if (db.AmazonMarketplaces.Any(o => o.AmazonCurrencyID == id))
            {
                TempData["DeleteRelatedError"]="Related entries exist. Cannot delete.";
                ViewBag.Error = "| " + TempData["DeleteRelatedError"];                        
                return View(amazoncurrency);
            }
            else
            {
                db.AmazonCurrencies.Remove(amazoncurrency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }        
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
