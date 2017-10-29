using ASF.UI.Process;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.UI.WbSite.Services.Cache;

namespace ASF.UI.WbSite.Areas.Product.Controllers
{
    public class ProductController : Controller
    {

        private DealerProcess dealer_process = new DealerProcess();
        // GET: Product/Product
        public ActionResult Index()
        {
            
            var product_process = new ProductProcess();
            return View(product_process.SelectList());
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ASF.Entities.Product c)
        {
            var product_process = new ProductProcess();
            c.Rowid = Guid.NewGuid();
            c.CreatedBy = 0;
            c.CreatedOn = DateTime.Now;
            c.ChangedOn = DateTime.Now;
            c.ChangedBy = 0;
            product_process.Add(c);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var product_process = new ProductProcess();
            return View(product_process.Find(id));
        }

        public ActionResult Edit(int id)
        {
            var product_process = new ProductProcess();
            var product = product_process.Find(id);
            TempData["product"] = product;
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(ASF.Entities.Product c)
        {
            var product_process = new ProductProcess();
            var old_product = (ASF.Entities.Product)TempData["product"];
            c.Id = old_product.Id;
            c.CreatedOn = old_product.CreatedOn;
            c.CreatedBy = old_product.CreatedBy;
            c.ChangedOn = DateTime.Now;
            c.ChangedBy = 0;
            product_process.Edit(c);
            return RedirectToAction("Details", new { id = c.Id });
        }

        public ActionResult Delete(int id)
        {
            var product_process = new ProductProcess();
            var product = product_process.Find(id);
            TempData["product"] = product;
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(ASF.Entities.Product c)
        {
            var product_process = new ProductProcess();
            c.Id = ((ASF.Entities.Product)TempData["product"]).Id;
            product_process.Delete(c.Id);
            return RedirectToAction("Index");
        }
        
        public JsonResult GetDealers(string term) {
            List<Entities.Dealer> dealers = dealer_process.GetByPattern(term);
            return Json(dealers, JsonRequestBehavior.AllowGet);
        }
    }
}