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
    public class AmazonKeywordController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonKeyword/
        public ActionResult Index()
        {
            return View(db.AmazonKeywords.ToList());
        }

        // GET: /AmazonKeyword/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonKeyword amazonkeyword = db.AmazonKeywords.Find(id);
            if (amazonkeyword == null)
            {
                return HttpNotFound();
            }
            return View(amazonkeyword);
        }

        // GET: /AmazonKeyword/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonKeyword/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonKeywordID,Keyword")] AmazonKeyword amazonkeyword)
        {
            if (ModelState.IsValid)
            {
                db.AmazonKeywords.Add(amazonkeyword);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazonkeyword);
        }

        // GET: /AmazonKeyword/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonKeyword amazonkeyword = db.AmazonKeywords.Find(id);
            if (amazonkeyword == null)
            {
                return HttpNotFound();
            }
            return View(amazonkeyword);
        }

        // POST: /AmazonKeyword/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonKeywordID,Keyword")] AmazonKeyword amazonkeyword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonkeyword).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazonkeyword);
        }

        // GET: /AmazonKeyword/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonKeyword amazonkeyword = db.AmazonKeywords.Find(id);
            if (amazonkeyword == null)
            {
                return HttpNotFound();
            }
            return View(amazonkeyword);
        }

        // POST: /AmazonKeyword/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonKeyword amazonkeyword = db.AmazonKeywords.Find(id);
            db.AmazonKeywords.Remove(amazonkeyword);
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
