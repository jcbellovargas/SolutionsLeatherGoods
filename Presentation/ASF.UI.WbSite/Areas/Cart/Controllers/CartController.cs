using ASF.UI.Process;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.UI.WbSite.Services.Cache;

namespace ASF.UI.WbSite.Areas.Cart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart/Cart
        public ActionResult Index()
        {
            
            var cart_process = new CartProcess();

            return View(cart_process.SelectList());
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
        public ActionResult Create(ASF.Entities.Cart cart)
        {
            var cart_process = new CartProcess();
            cart.CreatedOn = DateTime.Now;
            cart.CreatedBy = 1;
            cart.ChangedBy = 1;
            cart.ChangedOn = DateTime.Now;
            cart.Rowid = Guid.NewGuid();
            cart_process.Add(cart);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var cart_process = new CartProcess();
            return View(cart_process.Find(id));
        }

        public ActionResult Edit(int id)
        {
            var cart_process = new CartProcess();
            var cart = cart_process.Find(id);
            TempData["cart"] = cart;
            return View(cart);
        }

        [HttpPost]
        public ActionResult Edit(ASF.Entities.Cart cart)
        {
            var cart_process = new CartProcess();
            ASF.Entities.Cart old_cart = ((ASF.Entities.Cart)TempData["cart"]);
            cart.Id = old_cart.Id;
            cart.ChangedOn = DateTime.Now;
            cart.Rowid = old_cart.Rowid;
            cart.CreatedBy = old_cart.CreatedBy;
            cart.CreatedOn = old_cart.CreatedOn;
            cart.ChangedBy = 1;
            cart_process.Edit(cart);
            return RedirectToAction("Details", new { id = cart.Id });
        }

        public ActionResult Delete(int id)
        {
            var cart_process = new CartProcess();
            var cart = cart_process.Find(id);
            TempData["cart"] = cart;
            return View(cart);
        }

        [HttpPost]
        public ActionResult Delete(ASF.Entities.Cart cart)
        {
            var cart_process = new CartProcess();
            cart.Id = ((ASF.Entities.Cart)TempData["cart"]).Id;
            cart_process.Delete(cart.Id);
            return RedirectToAction("Index");
        }
    }
}