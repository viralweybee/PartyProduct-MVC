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
    public class PartiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Parties
        [Authorize]
        public ActionResult Index(string sortOrder,string searchString)
        {
            var parties = from p in db.parties select p;
            
            //searching functionalites
            if (!String.IsNullOrEmpty(searchString))
            {
                parties = parties.Where(s => s.p_name.Contains(searchString)||s.p_name.Contains(searchString));
            }
            //sorting functionalites
            ViewBag.NameSortParm = sortOrder =="name_asc" ?"name_desc" : "name_asc";
            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "id_asc" : "";
            switch (sortOrder)
            {
                case "name_desc":
                    parties = parties.OrderByDescending(p => p.p_name);
                    break;
                case "name_asc":
                    parties = parties.OrderBy(p => p.p_name);
                    break;
                case "id_asc":
                    parties = parties.OrderBy(p => p.id);
                    break;
                default:
                    parties = parties.OrderBy(p => p.id);
                    break;
            }
            return View(parties.ToList());
        }

        // GET: Parties/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Party party = db.parties.Find(id);
            if (party == null)
            {
                return HttpNotFound();
            }
            return View(party);
        }

        // GET: Parties/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,p_name")] Party party)
        {
            if (ModelState.IsValid)
            {
                db.parties.Add(party);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(party);
        }

        // GET: Parties/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Party party = db.parties.Find(id);
            if (party == null)
            {
                return HttpNotFound();
            }
            return View(party);
        }

        // POST: Parties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,p_name")] Party party)
        {
            if (ModelState.IsValid)
            {
                db.Entry(party).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(party);
        }

        // GET: Parties/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Party party = db.parties.Find(id);
            if (party == null)
            {
                return HttpNotFound();
            }
            return View(party);
        }

        // POST: Parties/Delete/5
        //[HttpPost, ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Party party = db.parties.Find(id);
            db.parties.Remove(party);
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
