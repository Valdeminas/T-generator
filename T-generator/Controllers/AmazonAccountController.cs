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
    public class AmazonAccountController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /Accounts/
        public ActionResult Index()
        {
            return View(db.AmazonAccounts.ToList());
        }

        // GET: /Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonAccount amazonAccount = db.AmazonAccounts.Find(id);
            if (amazonAccount == null)
            {
                return HttpNotFound();
            }
            return View(amazonAccount);
        }

        // GET: /Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BrandName,SKUPrefix")] AmazonAccount amazonAccount)
        {
            if (ModelState.IsValid)
            {
                db.AmazonAccounts.Add(amazonAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazonAccount);
        }

        // GET: /Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonAccount amazonAccount = db.AmazonAccounts.Find(id);
            if (amazonAccount == null)
            {
                return HttpNotFound();
            }
            return View(amazonAccount);
        }

        // POST: /Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BrandName,SKUPrefix")] AmazonAccount amazonAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazonAccount);
        }

        // GET: /Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonAccount amazonAccount = db.AmazonAccounts.Find(id);
            if (amazonAccount == null)
            {
                return HttpNotFound();
            }
            return View(amazonAccount);
        }

        // POST: /Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonAccount amazonAccount = db.AmazonAccounts.Find(id);
            db.AmazonAccounts.Remove(amazonAccount);
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
