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
        [Authorize]
        public ActionResult Index()
        {
            var list = db.parties.ToList();
            ViewBag.list1 = new SelectList(list, "id", "p_name");

            var dataInvoices = DataInvoice();
         
            var model = new BindInvoice();
            model.listinvoice = dataInvoices;
            
            return View(model);
        }
        //Post : Invoice
        [HttpPost]
        public ActionResult Index( BindInvoice invoice)
        {

            if (ModelState.IsValid)
            {
                var pId = Convert.ToInt32(invoice.simpleinvoice.Party.p_name);
                var prId = Convert.ToInt32(invoice.simpleinvoice.Product.pr_name);
                var party = db.parties.Single(x => x.id == pId);
                var product = db.products.Single(x => x.id == prId);
                Invoice i = new Invoice()
                {
                    rate = invoice.simpleinvoice.rate,
                    quantity = invoice.simpleinvoice.quantity,
                    Party = party,
                    Product = product
                };
                db.invoices.Add(i);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var inoviceData = db.invoices.Include(a => a.Party).Include(a => a.Product).ToList();
            var list = db.parties.ToList();
            ViewBag.list1 = new SelectList(list, "id", "p_name");
            return View();
        }
        //Close : Invoice 
        //Delete all data of particular party and starting with new party
        [HttpPost]
        public ActionResult Close(Invoice invoice)
        {
            db.invoices.RemoveRange(db.invoices);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Ajax Call
        //Onchange of Party also change a product List
        public JsonResult ProductList(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var productDetails = (from p in db.products
                                 join a in db.assigns on p.id equals a.ProductId
                                 where a.PartyId == id
                                 select new { p.id, p.pr_name }).ToList();
           
            return Json(productDetails, JsonRequestBehavior.AllowGet);
        }
        //Ajax Call of Rate
        //OnChange of Product Display latest date of rate for particular product
        public JsonResult Rate(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var productRate = (from p in db.product_Rates where p.Pr_id == id orderby p.date descending select p.rate).First();
            return Json(productRate, JsonRequestBehavior.AllowGet);

        }
        private List<Invoice> DataInvoice()
        {
            return db.invoices.Include(a => a.Party).Include(a => a.Product).ToList();
        }
    }
}