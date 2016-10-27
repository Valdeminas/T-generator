using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using T_generator.Models.Amazon;
using T_generator.DAL;

namespace T_generator.Controllers.Amazon.Data.Basic
{
    public class AmazonSizeController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonSize/
        public ActionResult Index()
        {
            return View(db.AmazonSizes.ToList());
        }

        // GET: /AmazonSize/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonSize amazonsize = db.AmazonSizes.Find(id);
            if (amazonsize == null)
            {
                return HttpNotFound();
            }
            return View(amazonsize);
        }

        // GET: /AmazonSize/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonSize/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonSizeID,Name,Prefix")] AmazonSize amazonsize)
        {
            if (ModelState.IsValid)
            {
                db.AmazonSizes.Add(amazonsize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazonsize);
        }

        // GET: /AmazonSize/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonSize amazonsize = db.AmazonSizes.Find(id);
            if (amazonsize == null)
            {
                return HttpNotFound();
            }
            return View(amazonsize);
        }

        // POST: /AmazonSize/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonSizeID,Name,Prefix")] AmazonSize amazonsize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonsize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazonsize);
        }

        // GET: /AmazonSize/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonSize amazonsize = db.AmazonSizes.Find(id);
            if (amazonsize == null)
            {
                return HttpNotFound();
            }
            return View(amazonsize);
        }

        // POST: /AmazonSize/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonSize amazonsize = db.AmazonSizes.Find(id);
            db.AmazonSizes.Remove(amazonsize);
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
