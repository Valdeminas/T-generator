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
    public class AmazonVariationController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonVariation/
        public ActionResult Index()
        {
            return View(db.AmazonVariations.ToList());
        }

        // GET: /AmazonVariation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonVariation amazonvariation = db.AmazonVariations.Find(id);
            if (amazonvariation == null)
            {
                return HttpNotFound();
            }
            return View(amazonvariation);
        }

        // GET: /AmazonVariation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonVariation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonVariationID,Name,Prefix")] AmazonVariation amazonvariation)
        {
            if (ModelState.IsValid)
            {
                db.AmazonVariations.Add(amazonvariation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazonvariation);
        }

        // GET: /AmazonVariation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonVariation amazonvariation = db.AmazonVariations.Find(id);
            if (amazonvariation == null)
            {
                return HttpNotFound();
            }
            return View(amazonvariation);
        }

        // POST: /AmazonVariation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonVariationID,Name,Prefix")] AmazonVariation amazonvariation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonvariation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazonvariation);
        }

        // GET: /AmazonVariation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonVariation amazonvariation = db.AmazonVariations.Find(id);
            if (amazonvariation == null)
            {
                return HttpNotFound();
            }
            return View(amazonvariation);
        }

        // POST: /AmazonVariation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonVariation amazonvariation = db.AmazonVariations.Find(id);
            db.AmazonVariations.Remove(amazonvariation);
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
