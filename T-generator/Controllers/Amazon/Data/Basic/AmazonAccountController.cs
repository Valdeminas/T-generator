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
    public class AmazonAccountController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonAccount/
        public ActionResult Index()
        {
            return View(db.AmazonAccounts.ToList());
        }

        // GET: /AmazonAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonAccount amazonaccount = db.AmazonAccounts.Find(id);
            if (amazonaccount == null)
            {
                return HttpNotFound();
            }
            return View(amazonaccount);
        }

        // GET: /AmazonAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonAccountID,Name,Prefix")] AmazonAccount amazonaccount)
        {
            if (ModelState.IsValid)
            {
                db.AmazonAccounts.Add(amazonaccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazonaccount);
        }

        // GET: /AmazonAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonAccount amazonaccount = db.AmazonAccounts.Find(id);
            if (amazonaccount == null)
            {
                return HttpNotFound();
            }
            return View(amazonaccount);
        }

        // POST: /AmazonAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonAccountID,Name,Prefix")] AmazonAccount amazonaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonaccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazonaccount);
        }

        // GET: /AmazonAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonAccount amazonaccount = db.AmazonAccounts.Find(id);
            if (amazonaccount == null)
            {
                return HttpNotFound();
            }
            return View(amazonaccount);
        }

        // POST: /AmazonAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonAccount amazonaccount = db.AmazonAccounts.Find(id);
            db.AmazonAccounts.Remove(amazonaccount);
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
