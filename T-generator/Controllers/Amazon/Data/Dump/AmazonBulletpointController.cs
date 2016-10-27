using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.DAL;

namespace T_generator.Controllers.Amazon.Data.Dump
{
    public class AmazonBulletPointController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonBulletpoint/
        public ActionResult Index()
        {
            return View(db.AmazonBulletPoints.ToList());
        }

        // GET: /AmazonBulletpoint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonBulletPoint amazonbulletpoint = db.AmazonBulletPoints.Find(id);
            if (amazonbulletpoint == null)
            {
                return HttpNotFound();
            }
            return View(amazonbulletpoint);
        }

        // GET: /AmazonBulletpoint/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonBulletpoint/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonBulletPointID,BulletPoint")] AmazonBulletPoint amazonbulletpoint)
        {
            if (ModelState.IsValid)
            {
                db.AmazonBulletPoints.Add(amazonbulletpoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazonbulletpoint);
        }

        // GET: /AmazonBulletpoint/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonBulletPoint amazonbulletpoint = db.AmazonBulletPoints.Find(id);
            if (amazonbulletpoint == null)
            {
                return HttpNotFound();
            }
            return View(amazonbulletpoint);
        }

        // POST: /AmazonBulletpoint/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonBulletPointID,BulletPoint")] AmazonBulletPoint amazonbulletpoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonbulletpoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazonbulletpoint);
        }

        // GET: /AmazonBulletpoint/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonBulletPoint amazonbulletpoint = db.AmazonBulletPoints.Find(id);
            if (amazonbulletpoint == null)
            {
                return HttpNotFound();
            }
            return View(amazonbulletpoint);
        }

        // POST: /AmazonBulletpoint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonBulletPoint amazonbulletpoint = db.AmazonBulletPoints.Find(id);
            db.AmazonBulletPoints.Remove(amazonbulletpoint);
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
