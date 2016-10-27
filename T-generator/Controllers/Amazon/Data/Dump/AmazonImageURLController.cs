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
    public class AmazonImageURLController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonImageURL/
        public ActionResult Index()
        {
            return View(db.AmazonImageURLs.ToList());
        }

        // GET: /AmazonImageURL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonImageURL amazonimageurl = db.AmazonImageURLs.Find(id);
            if (amazonimageurl == null)
            {
                return HttpNotFound();
            }
            return View(amazonimageurl);
        }

        // GET: /AmazonImageURL/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonImageURL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonImageURLID,ImageURL")] AmazonImageURL amazonimageurl)
        {
            if (ModelState.IsValid)
            {
                db.AmazonImageURLs.Add(amazonimageurl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazonimageurl);
        }

        // GET: /AmazonImageURL/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonImageURL amazonimageurl = db.AmazonImageURLs.Find(id);
            if (amazonimageurl == null)
            {
                return HttpNotFound();
            }
            return View(amazonimageurl);
        }

        // POST: /AmazonImageURL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonImageURLID,ImageURL")] AmazonImageURL amazonimageurl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonimageurl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazonimageurl);
        }

        // GET: /AmazonImageURL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonImageURL amazonimageurl = db.AmazonImageURLs.Find(id);
            if (amazonimageurl == null)
            {
                return HttpNotFound();
            }
            return View(amazonimageurl);
        }

        // POST: /AmazonImageURL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonImageURL amazonimageurl = db.AmazonImageURLs.Find(id);
            db.AmazonImageURLs.Remove(amazonimageurl);
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
