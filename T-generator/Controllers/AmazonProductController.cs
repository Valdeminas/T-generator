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
    public class AmazonProductController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonProduct/
        public ActionResult Index()
        {
            return View(db.AmazonProducts.ToList());
        }

        // GET: /AmazonProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonProduct amazonproduct = db.AmazonProducts.Find(id);
            if (amazonproduct == null)
            {
                return HttpNotFound();
            }
            return View(amazonproduct);
        }

        // GET: /AmazonProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Title,Description,MetaDescription,Type,TypeShort,BrowseNode,DepartmentName,StaticSearchtags,BulletPoints,Template,Size")] AmazonProduct amazonproduct)
        {
            if (ModelState.IsValid)
            {
                db.AmazonProducts.Add(amazonproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazonproduct);
        }

        // GET: /AmazonProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonProduct amazonproduct = db.AmazonProducts.Find(id);
            if (amazonproduct == null)
            {
                return HttpNotFound();
            }
            return View(amazonproduct);
        }

        // POST: /AmazonProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonProductID,Title,Description,MetaDescription,Type,TypeShort,BrowseNode,DepartmentName,StaticSearchtags,BulletPoints,Template,Size")] AmazonProduct amazonproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazonproduct);
        }

        // GET: /AmazonProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonProduct amazonproduct = db.AmazonProducts.Find(id);
            if (amazonproduct == null)
            {
                return HttpNotFound();
            }
            return View(amazonproduct);
        }

        // POST: /AmazonProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonProduct amazonproduct = db.AmazonProducts.Find(id);
            db.AmazonProducts.Remove(amazonproduct);
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
