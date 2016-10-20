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

namespace T_generator.Controllers
{
    public class AmazonColorVariationController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonColorVariation/
        public ActionResult Index()
        {
            return View(db.AmazonColorVariations.ToList());
        }

        // GET: /AmazonColorVariation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonColorVariation amazoncolorvariation = db.AmazonColorVariations.Find(id);
            if (amazoncolorvariation == null)
            {
                return HttpNotFound();
            }
            return View(amazoncolorvariation);
        }

        // GET: /AmazonColorVariation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonColorVariation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonColorVariationID,Name,NameShort,Price,Image")] AmazonColorVariation amazoncolorvariation)
        {
            if (ModelState.IsValid)
            {
                db.AmazonColorVariations.Add(amazoncolorvariation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazoncolorvariation);
        }

        // GET: /AmazonColorVariation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonColorVariation amazoncolorvariation = db.AmazonColorVariations.Find(id);
            if (amazoncolorvariation == null)
            {
                return HttpNotFound();
            }
            return View(amazoncolorvariation);
        }

        // POST: /AmazonColorVariation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonColorVariationID,Name,NameShort,Price,Image")] AmazonColorVariation amazoncolorvariation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazoncolorvariation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazoncolorvariation);
        }

        // GET: /AmazonColorVariation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonColorVariation amazoncolorvariation = db.AmazonColorVariations.Find(id);
            if (amazoncolorvariation == null)
            {
                return HttpNotFound();
            }
            return View(amazoncolorvariation);
        }

        // POST: /AmazonColorVariation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonColorVariation amazoncolorvariation = db.AmazonColorVariations.Find(id);
            db.AmazonColorVariations.Remove(amazoncolorvariation);
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
