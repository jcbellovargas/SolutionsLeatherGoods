using ASF.UI.Process;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.Entities;

namespace ASF.UI.WbSite.Areas.Shop.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private ProductProcess product_process = new ProductProcess();
        private CartProcess cart_process = new CartProcess();
        private CartItemProcess cart_item_process = new CartItemProcess();
        // GET: Shop/Shop
        public ActionResult Index()
        {
            var products = product_process.SelectList();
            ViewBag.products = JsonConvert.SerializeObject(products);
            var current_session = Request.Cookies["session"];
            if (current_session == null) {
                var cookie = new HttpCookie("session");
                cookie.Values["token"] = HttpContext.Session.SessionID;
                cookie.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Add(cookie);
                var current_cart = NuevoCart();
                cart_process.Add(current_cart);
            } else {
                var current_cart = cart_process.SelectByCookie(current_session["token"].ToString());
                if (current_cart != null) {
                    var current_cart_items = cart_item_process.GetAllByCartId(current_cart.Id);
                    ViewBag.cart = JsonConvert.SerializeObject(current_cart_items);
                } else {
                    current_cart = NuevoCart();
                    cart_process.Add(current_cart);
                }
            }
            return View();
        }

        [HttpGet]
        public JsonResult Buscar(string pattern) {
            var products = pattern == "" ? product_process.SelectList() : product_process.GetByName(pattern);
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AgregarAlCarro() {
            var cart_item = NuevoCartItem();
            cart_item.ProductId = Int32.Parse(Request.Form["ProductId"]);
            cart_item.Quantity = Int32.Parse(Request.Form["Quantity"]);
            cart_item.Price = float.Parse(Request.Form["Price"]);
            Entities.Cart current_cart = cart_process.SelectByCookie(Request.Cookies["session"]["token"].ToString());
            cart_item.CartId = current_cart.Id;
            current_cart.Items.Add(cart_item);
            cart_process.Edit(current_cart);
            return null;
        }

        private Entities.Cart NuevoCart() {
            var cart = new Entities.Cart();
            cart.Items = new List<Entities.CartItem>();
            cart.CartDate = DateTime.Now;
            cart.ChangedBy = 0;
            cart.ChangedOn = cart.CartDate;
            cart.CreatedBy = 0;
            cart.CreatedOn = cart.CartDate;
            cart.Rowid = Guid.NewGuid();
            cart.Cookie = Request.Cookies["session"]["token"].ToString();
            return cart;
        }

        private Entities.CartItem NuevoCartItem() {
            var cart_item = new Entities.CartItem();
            cart_item.ChangedBy = 0;
            cart_item.ChangedOn = DateTime.Now;
            cart_item.CreatedBy = 0;
            cart_item.CreatedOn = cart_item.ChangedOn;
            return cart_item;
        }
    }
}