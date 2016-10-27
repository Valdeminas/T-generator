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
    public class AmazonBrowseNodeController : Controller
    {
        private AmazonContext db = new AmazonContext();

        // GET: /AmazonBrowsenode/
        public ActionResult Index()
        {
            return View(db.AmazonBrowseNodes.ToList());
        }

        // GET: /AmazonBrowsenode/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonBrowseNode amazonbrowsenode = db.AmazonBrowseNodes.Find(id);
            if (amazonbrowsenode == null)
            {
                return HttpNotFound();
            }
            return View(amazonbrowsenode);
        }

        // GET: /AmazonBrowsenode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AmazonBrowsenode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AmazonBrowseNodeID,BrowseNode")] AmazonBrowseNode amazonbrowsenode)
        {
            if (ModelState.IsValid)
            {
                db.AmazonBrowseNodes.Add(amazonbrowsenode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amazonbrowsenode);
        }

        // GET: /AmazonBrowsenode/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonBrowseNode amazonbrowsenode = db.AmazonBrowseNodes.Find(id);
            if (amazonbrowsenode == null)
            {
                return HttpNotFound();
            }
            return View(amazonbrowsenode);
        }

        // POST: /AmazonBrowsenode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AmazonBrowseNodeID,BrowseNode")] AmazonBrowseNode amazonbrowsenode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amazonbrowsenode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amazonbrowsenode);
        }

        // GET: /AmazonBrowsenode/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmazonBrowseNode amazonbrowsenode = db.AmazonBrowseNodes.Find(id);
            if (amazonbrowsenode == null)
            {
                return HttpNotFound();
            }
            return View(amazonbrowsenode);
        }

        // POST: /AmazonBrowsenode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmazonBrowseNode amazonbrowsenode = db.AmazonBrowseNodes.Find(id);
            db.AmazonBrowseNodes.Remove(amazonbrowsenode);
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
