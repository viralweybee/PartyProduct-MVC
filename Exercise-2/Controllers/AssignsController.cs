using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exercise_2.Models;

namespace Exercise_2.Controllers
{
    public class AssignsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assigns
        [Authorize]
        public ActionResult Index()
        {
            var assigns = db.assigns.Include(a => a.Party).Include(a => a.Product);
            return View(assigns.ToList());
        }

        // GET: Assigns/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assign assign = db.assigns.Find(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            return View(assign);
        }

        // GET: Assigns/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PartyId = new SelectList(db.parties, "id", "p_name");
            ViewBag.ProductId = new SelectList(db.products, "id", "pr_name");
            return View();
        }

        // POST: Assigns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,PartyId,ProductId")] Assign assign)
        {
            if (ModelState.IsValid)
            {
                db.assigns.Add(assign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PartyId = new SelectList(db.parties, "id", "p_name", assign.PartyId);
            ViewBag.ProductId = new SelectList(db.products, "id", "pr_name", assign.ProductId);
            return View(assign);
        }

        // GET: Assigns/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assign assign = db.assigns.Find(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartyId = new SelectList(db.parties, "id", "p_name", assign.PartyId);
            ViewBag.ProductId = new SelectList(db.products, "id", "pr_name", assign.ProductId);
            return View(assign);
        }

        // POST: Assigns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,PartyId,ProductId")] Assign assign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PartyId = new SelectList(db.parties, "id", "p_name", assign.PartyId);
            ViewBag.ProductId = new SelectList(db.products, "id", "pr_name", assign.ProductId);
            return View(assign);
        }

        [Authorize]
        // GET: Assigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assign assign = db.assigns.Find(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            return View(assign);
        }

        // POST: Assigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assign assign = db.assigns.Find(id);
            db.assigns.Remove(assign);
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
