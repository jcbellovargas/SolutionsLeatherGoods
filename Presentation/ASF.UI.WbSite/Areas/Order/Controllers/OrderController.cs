using ASF.UI.Process;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.UI.WbSite.Services.Cache;

namespace ASF.UI.WbSite.Areas.Order.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order/Order
        public ActionResult Index()
        {
            
            var order_process = new OrderProcess();

            return View(order_process.SelectList());
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
        public ActionResult Create(ASF.Entities.Order order)
        {
            var order_process = new OrderProcess();
            order.CreatedOn = DateTime.Now;
            order.CreatedBy = 1;
            order.ChangedBy = 1;
            order.ChangedOn = DateTime.Now;
            order.Rowid = Guid.NewGuid();
            order_process.Add(order);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var order_process = new OrderProcess();
            return View(order_process.Find(id));
        }

        public ActionResult Edit(int id)
        {
            var order_process = new OrderProcess();
            var order = order_process.Find(id);
            TempData["order"] = order;
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(ASF.Entities.Order order)
        {
            var order_process = new OrderProcess();
            ASF.Entities.Order old_order = ((ASF.Entities.Order)TempData["order"]);
            order.Id = old_order.Id;
            order.ChangedOn = DateTime.Now;
            order.Rowid = old_order.Rowid;
            order.CreatedBy = old_order.CreatedBy;
            order.CreatedOn = old_order.CreatedOn;
            order.ChangedBy = 1;
            order_process.Edit(order);
            return RedirectToAction("Details", new { id = order.Id });
        }

        public ActionResult Delete(int id)
        {
            var order_process = new OrderProcess();
            var order = order_process.Find(id);
            TempData["order"] = order;
            return View(order);
        }

        [HttpPost]
        public ActionResult Delete(ASF.Entities.Order order)
        {
            var order_process = new OrderProcess();
            order.Id = ((ASF.Entities.Order)TempData["order"]).Id;
            order_process.Delete(order.Id);
            return RedirectToAction("Index");
        }
    }
}