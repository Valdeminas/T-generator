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
    public class AmazonTypeController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonType/
        public ActionResult Index()
        {
            return View(db.AmazonTypes.ToList());
        }

        // GET: /AmazonType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonType amazontype = db.AmazonTypes.Find(id);
            if (amazontype == null)
            {
                return HttpNotFound();
            }
            return View(amazontype);
        }

        // GET: /AmazonType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonTypeID,Name,Prefix")] AmazonType amazontype)
        {
            if (ModelState.IsValid)
            {
                db.AmazonTypes.Add(amazontype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazontype);
        }

        // GET: /AmazonType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonType amazontype = db.AmazonTypes.Find(id);
            if (amazontype == null)
            {
                return HttpNotFound();
            }
            return View(amazontype);
        }

        // POST: /AmazonType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonTypeID,Name,Prefix")] AmazonType amazontype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazontype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazontype);
        }

        // GET: /AmazonType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonType amazontype = db.AmazonTypes.Find(id);
            if (amazontype == null)
            {
                return HttpNotFound();
            }
            return View(amazontype);
        }

        // POST: /AmazonType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonType amazontype = db.AmazonTypes.Find(id);

            if (db.AmazonProducts.Any(o => o.AmazonTypeID == id))
            {
                TempData["DeleteRelatedError"] = "Related entries exist. Cannot delete.";
                ViewBag.Error = "| " + TempData["DeleteRelatedError"];
                return View(amazontype);
            }
            else
            {
                db.AmazonTypes.Remove(amazontype);
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
