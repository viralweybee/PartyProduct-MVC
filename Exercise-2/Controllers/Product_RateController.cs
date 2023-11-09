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
    public class Product_RateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product_Rate
        public ActionResult Index()
        {
            var product_Rates = db.product_Rates.Include(p => p.Product);
            return View(product_Rates.ToList());
        }

        // GET: Product_Rate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Rate product_Rate = db.product_Rates.Find(id);
            if (product_Rate == null)
            {
                return HttpNotFound();
            }
            return View(product_Rate);
        }

        // GET: Product_Rate/Create
        public ActionResult Create()
        {
            ViewBag.Pr_id = new SelectList(db.products, "id", "pr_name");
            return View();
        }

        // POST: Product_Rate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Pr_id,rate,date")] Product_Rate product_Rate)
        {
            if (ModelState.IsValid)
            {
                db.product_Rates.Add(product_Rate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pr_id = new SelectList(db.products, "id", "pr_name", product_Rate.Pr_id);
            return View(product_Rate);
        }

        // GET: Product_Rate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Rate product_Rate = db.product_Rates.Find(id);
            if (product_Rate == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pr_id = new SelectList(db.products, "id", "pr_name", product_Rate.Pr_id);
            return View(product_Rate);
        }

        // POST: Product_Rate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Pr_id,rate,date")] Product_Rate product_Rate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pr_id = new SelectList(db.products, "id", "pr_name", product_Rate.Pr_id);
            return View(product_Rate);
        }

        // GET: Product_Rate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Rate product_Rate = db.product_Rates.Find(id);
            if (product_Rate == null)
            {
                return HttpNotFound();
            }
            return View(product_Rate);
        }

        // POST: Product_Rate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Rate product_Rate = db.product_Rates.Find(id);
            db.product_Rates.Remove(product_Rate);
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
