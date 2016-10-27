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

namespace T_generator.Controllers.Amazon.Data.Basic
{
    public class AmazonCountryController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonCountry/
        public ActionResult Index()
        {
            return View(db.AmazonCountries.ToList());
        }

        // GET: /AmazonCountry/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonCountry amazoncountry = db.AmazonCountries.Find(id);
            if (amazoncountry == null)
            {
                return HttpNotFound();
            }
            return View(amazoncountry);
        }

        // GET: /AmazonCountry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonCountry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonCountryID,Name,Prefix")] AmazonCountry amazoncountry)
        {
            if (ModelState.IsValid)
            {
                db.AmazonCountries.Add(amazoncountry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazoncountry);
        }

        // GET: /AmazonCountry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonCountry amazoncountry = db.AmazonCountries.Find(id);
            if (amazoncountry == null)
            {
                return HttpNotFound();
            }
            return View(amazoncountry);
        }

        // POST: /AmazonCountry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonCountryID,Name,Prefix")] AmazonCountry amazoncountry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazoncountry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazoncountry);
        }

        // GET: /AmazonCountry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonCountry amazoncountry = db.AmazonCountries.Find(id);
            if (amazoncountry == null)
            {
                return HttpNotFound();
            }
            return View(amazoncountry);
        }

        // POST: /AmazonCountry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonCountry amazoncountry = db.AmazonCountries.Find(id);
            db.AmazonCountries.Remove(amazoncountry);
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
    }
}
