using ASF.UI.Process;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASF.UI.WbSite.Areas.Shop.Controllers
{
    public class ShopController : Controller
    {
        private ProductProcess product_process = new ProductProcess();
        // GET: Shop/Shop
        public ActionResult Index()
        {
            var products = product_process.SelectList();
            ViewBag.products = JsonConvert.SerializeObject(products);
            return View();
        }

        [HttpGet]
        public JsonResult Buscar(string pattern) {
            var products = pattern == "" ? product_process.SelectList() : product_process.GetByName(pattern);
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}