using ASF.UI.Process;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.UI.WbSite.Services.Cache;

namespace ASF.UI.WbSite.Areas.Dealer.Controllers
{
    public class DealerController : Controller
    {

        private CategoryProcess category_process = new CategoryProcess();
        // GET: Dealer/Dealer
        public ActionResult Index()
        {
            
            var dealer_process = new DealerProcess();

            return View(dealer_process.SelectList());
        }

        public ActionResult Create()
        {
            var category_process = new CategoryProcess();
            var country_process = new CountryProcess();
            ViewBag.Categories = new SelectList(category_process.SelectList(), "Id", "Name");
            ViewBag.Countries = new SelectList(country_process.SelectList(), "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ASF.Entities.Dealer dealer)
        {
            var dealer_process = new DealerProcess();
            dealer.CreatedOn = DateTime.Now;
            dealer.CreatedBy = 1;
            dealer.ChangedBy = 1;
            dealer.ChangedOn = DateTime.Now;
            dealer.Rowid = Guid.NewGuid();
            dealer_process.Add(dealer);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var dealer_process = new DealerProcess();
            return View(dealer_process.Find(id));
        }

        public ActionResult Edit(int id)
        {
            var dealer_process = new DealerProcess();
            var dealer = dealer_process.Find(id);
            TempData["dealer"] = dealer;
            return View(dealer);
        }

        [HttpPost]
        public ActionResult Edit(ASF.Entities.Dealer dealer)
        {
            var dealer_process = new DealerProcess();
            ASF.Entities.Dealer old_dealer = ((ASF.Entities.Dealer)TempData["dealer"]);
            dealer.Id = old_dealer.Id;
            dealer.ChangedOn = DateTime.Now;
            dealer.Rowid = old_dealer.Rowid;
            dealer.CreatedBy = old_dealer.CreatedBy;
            dealer.CreatedOn = old_dealer.CreatedOn;
            dealer.ChangedBy = 1;
            dealer_process.Edit(dealer);
            return RedirectToAction("Details", new { id = dealer.Id });
        }

        public ActionResult Delete(int id)
        {
            var dealer_process = new DealerProcess();
            var dealer = dealer_process.Find(id);
            TempData["dealer"] = dealer;
            return View(dealer);
        }

        [HttpPost]
        public ActionResult Delete(ASF.Entities.Dealer dealer)
        {
            var dealer_process = new DealerProcess();
            dealer.Id = ((ASF.Entities.Dealer)TempData["dealer"]).Id;
            dealer_process.Delete(dealer.Id);
            return RedirectToAction("Index");
        }

        public JsonResult GetCategories(string term) {
            List<Entities.Category> dealers = category_process.GetByPattern(term);
            return Json(dealers, JsonRequestBehavior.AllowGet);
        }
    }
}