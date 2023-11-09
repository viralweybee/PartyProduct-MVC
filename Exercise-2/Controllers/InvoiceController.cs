using Exercise_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Exercise_2.Controllers
{
    public class InvoiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Invoice

        public ActionResult Index()
        {
            var list = db.parties.ToList();
            ViewBag.list1 = new SelectList(list, "id", "p_name");
            return View();
        }
        [HttpPost]
        public ActionResult Index([Bind(Include = "id,PartyId,ProductId,rate,time")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.invoices.Add(invoice);
                db.SaveChanges();
            }
            var inoviceData = db.invoices.Include(a => a.Party).Include(a => a.Product).ToList();
            return View(inoviceData);
        }
        [HttpGet]
        public ActionResult DisplayInvoice()
        {
            var inoviceData = db.invoices.Include(a => a.Party).Include(a => a.Product).ToList();
            return View(inoviceData);
        }
        [HttpPost]
        public ActionResult DisplayInvoice([Bind(Include = "id,PartyId,ProductId,rate,time")] Invoice invoice)
        {   
            if (ModelState.IsValid)
            {
                db.invoices.Add(invoice);
                db.SaveChanges();
            }
            var inoviceData = db.invoices.Include(a => a.Party).Include(a => a.Product).ToList();
            return View(inoviceData);
        }
        public ActionResult Close(Invoice invoice)
        {
            /*db.invoices.Remove(invoice);
            db.SaveChanges();*/
            return View("DisplayInvoice");
        }
        public JsonResult ProductList(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var productDetails = (from p in db.products
                                 join a in db.assigns on p.id equals a.ProductId
                                 where a.PartyId == id
                                 select new { p.id, p.pr_name }).ToList();
           
            return Json(productDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Rate(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var productRate = (from p in db.product_Rates where p.Pr_id == id orderby p.date descending select p.rate).First();
            return Json(productRate, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ViewInvoice()
        {
            db.Configuration.ProxyCreationEnabled = false;

            var list1 = (from a in db.invoices
                         select new
                         {
                             a.id, a.Party.p_name, a.Product.pr_name, a.rate, a.time
                         }).ToList();
            return Json(list1, JsonRequestBehavior.AllowGet);
        }
    }
}