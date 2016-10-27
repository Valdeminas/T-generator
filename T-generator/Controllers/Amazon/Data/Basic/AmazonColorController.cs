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

namespace T_generator.Controllers.Amazon.Data.Basic
{
    public class AmazonColorController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonColor/
        public ActionResult Index()
        {
            return View(db.AmazonColors.ToList());
        }

        // GET: /AmazonColor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonColor amazoncolor = db.AmazonColors.Find(id);
            if (amazoncolor == null)
            {
                return HttpNotFound();
            }
            return View(amazoncolor);
        }

        // GET: /AmazonColor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonColor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonColorID,Name,Prefix")] AmazonColor amazoncolor)
        {
            if (ModelState.IsValid)
            {
                db.AmazonColors.Add(amazoncolor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazoncolor);
        }

        // GET: /AmazonColor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonColor amazoncolor = db.AmazonColors.Find(id);
            if (amazoncolor == null)
            {
                return HttpNotFound();
            }
            return View(amazoncolor);
        }

        // POST: /AmazonColor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonColorID,Name,Prefix")] AmazonColor amazoncolor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazoncolor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazoncolor);
        }

        // GET: /AmazonColor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonColor amazoncolor = db.AmazonColors.Find(id);
            if (amazoncolor == null)
            {
                return HttpNotFound();
            }
            return View(amazoncolor);
        }

        // POST: /AmazonColor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonColor amazoncolor = db.AmazonColors.Find(id);
            db.AmazonColors.Remove(amazoncolor);
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
