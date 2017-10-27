using ASF.UI.Process;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.UI.WbSite.Services.Cache;

namespace ASF.UI.WbSite.Areas.CartItem.Controllers
{
    public class CartItemController : Controller
    {
        // GET: CartItem/CartItem
        public ActionResult Index()
        {
            
            var cartItem_process = new CartItemProcess();

            return View(cartItem_process.SelectList());
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
        public ActionResult Create(ASF.Entities.CartItem cartItem)
        {
            var cartItem_process = new CartItemProcess();
            cartItem.CreatedOn = DateTime.Now;
            cartItem.CreatedBy = 1;
            cartItem.ChangedBy = 1;
            cartItem.ChangedOn = DateTime.Now;
            cartItem_process.Add(cartItem);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var cartItem_process = new CartItemProcess();
            return View(cartItem_process.Find(id));
        }

        public ActionResult Edit(int id)
        {
            var cartItem_process = new CartItemProcess();
            var cartItem = cartItem_process.Find(id);
            TempData["cartItem"] = cartItem;
            return View(cartItem);
        }

        [HttpPost]
        public ActionResult Edit(ASF.Entities.CartItem cartItem)
        {
            var cartItem_process = new CartItemProcess();
            ASF.Entities.CartItem old_cartItem = ((ASF.Entities.CartItem)TempData["cartItem"]);
            cartItem.Id = old_cartItem.Id;
            cartItem.ChangedOn = DateTime.Now;
            cartItem.CreatedBy = old_cartItem.CreatedBy;
            cartItem.CreatedOn = old_cartItem.CreatedOn;
            cartItem.ChangedBy = 1;
            cartItem_process.Edit(cartItem);
            return RedirectToAction("Details", new { id = cartItem.Id });
        }

        public ActionResult Delete(int id)
        {
            var cartItem_process = new CartItemProcess();
            var cartItem = cartItem_process.Find(id);
            TempData["cartItem"] = cartItem;
            return View(cartItem);
        }

        [HttpPost]
        public ActionResult Delete(ASF.Entities.CartItem cartItem)
        {
            var cartItem_process = new CartItemProcess();
            cartItem.Id = ((ASF.Entities.CartItem)TempData["cartItem"]).Id;
            cartItem_process.Delete(cartItem.Id);
            return RedirectToAction("Index");
        }
    }
}