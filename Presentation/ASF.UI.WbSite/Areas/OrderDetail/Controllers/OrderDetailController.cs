using ASF.UI.Process;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.UI.WbSite.Services.Cache;

namespace ASF.UI.WbSite.Areas.OrderDetail.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: OrderDetail/OrderDetail
        public ActionResult Index()
        {
            
            var orderDetail_process = new OrderDetailProcess();

            return View(orderDetail_process.SelectList());
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
        public ActionResult Create(ASF.Entities.OrderDetail orderDetail)
        {
            var orderDetail_process = new OrderDetailProcess();
            orderDetail.CreatedOn = DateTime.Now;
            orderDetail.CreatedBy = 1;
            orderDetail.ChangedBy = 1;
            orderDetail.ChangedOn = DateTime.Now;
            orderDetail_process.Add(orderDetail);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var orderDetail_process = new OrderDetailProcess();
            return View(orderDetail_process.Find(id));
        }

        public ActionResult Edit(int id)
        {
            var orderDetail_process = new OrderDetailProcess();
            var orderDetail = orderDetail_process.Find(id);
            TempData["orderDetail"] = orderDetail;
            return View(orderDetail);
        }

        [HttpPost]
        public ActionResult Edit(ASF.Entities.OrderDetail orderDetail)
        {
            var orderDetail_process = new OrderDetailProcess();
            ASF.Entities.OrderDetail old_orderDetail = ((ASF.Entities.OrderDetail)TempData["orderDetail"]);
            orderDetail.Id = old_orderDetail.Id;
            orderDetail.ChangedOn = DateTime.Now;
            orderDetail.CreatedBy = old_orderDetail.CreatedBy;
            orderDetail.CreatedOn = old_orderDetail.CreatedOn;
            orderDetail.ChangedBy = 1;
            orderDetail_process.Edit(orderDetail);
            return RedirectToAction("Details", new { id = orderDetail.Id });
        }

        public ActionResult Delete(int id)
        {
            var orderDetail_process = new OrderDetailProcess();
            var orderDetail = orderDetail_process.Find(id);
            TempData["orderDetail"] = orderDetail;
            return View(orderDetail);
        }

        [HttpPost]
        public ActionResult Delete(ASF.Entities.OrderDetail orderDetail)
        {
            var orderDetail_process = new OrderDetailProcess();
            orderDetail.Id = ((ASF.Entities.OrderDetail)TempData["orderDetail"]).Id;
            orderDetail_process.Delete(orderDetail.Id);
            return RedirectToAction("Index");
        }
    }
}