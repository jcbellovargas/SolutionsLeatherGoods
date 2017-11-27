using ASF.UI.Process;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.Entities;
using System.Dynamic;
using System.Collections.Specialized;

namespace ASF.UI.WbSite.Controllers {

    
    
    public class ShopController : Controller {
        public const int PAGE_SIZE = 6;
        private ProductProcess product_process = new ProductProcess();
        private CartProcess cart_process = new CartProcess();
        private CartItemProcess cart_item_process = new CartItemProcess();
        private ClientProcess client_process = new ClientProcess();
        private OrderProcess order_process = new OrderProcess();
        private OrderDetailProcess order_detail_process = new OrderDetailProcess();
        private Random random = new Random();

        public ActionResult Index() {
            List<CartItem> current_cart_items = null;
            var products = product_process.SelectList();
            ViewBag.page_count = Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(PAGE_SIZE));
            ViewBag.products = JsonConvert.SerializeObject(GetPage(products, 0, PAGE_SIZE));
            var current_session = Request.Cookies["session"];
            if (current_session == null) {
                var cookie = new HttpCookie("session");
                cookie.Values["token"] = HttpContext.Session.SessionID;
                cookie.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Add(cookie);
                var current_cart = NuevoCart();
                cart_process.Add(current_cart);
                ViewBag.cart = JsonConvert.SerializeObject(null);
            } else {
                var current_cart = cart_process.SelectByCookie(current_session["token"].ToString());
                if (current_cart != null) {
                    current_cart_items = cart_item_process.GetAllByCartId(current_cart.Id);
                } else {
                    current_cart = NuevoCart();
                    cart_process.Add(current_cart);
                }
            }
            ViewBag.cart = JsonConvert.SerializeObject(current_cart_items);
            return View();
        }

        [HttpGet]
        [Route("Shop/Buscar")]
        public JsonResult Buscar(string pattern) {
            var products = pattern == "" ? product_process.SelectList() : product_process.GetByName(pattern);
            dynamic response = new ExpandoObject();
            response.page_count = Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(PAGE_SIZE));
            response.products = GetPage(products, 0, PAGE_SIZE);
            return Json(response, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult RemoverDelCarro() {
            Entities.Cart current_cart = cart_process.SelectByCookie(Request.Cookies["session"]["token"].ToString());
            var cart_item = NuevoCartItem();
            cart_item.ProductId = Int32.Parse(Request.Form["ProductId"]);
            cart_item.Quantity = 0; // quantity = 0 para que sea eliminado
            current_cart.Items.Add(cart_item);
            cart_process.Edit(current_cart);
            return null;
        }

        [HttpPost]
        public ActionResult CambiarCantidad() {
            Entities.Cart current_cart = cart_process.SelectByCookie(Request.Cookies["session"]["token"].ToString());
            var cart_item = NuevoCartItem();
            cart_item.ProductId = Int32.Parse(Request.Form["ProductId"]);
            cart_item.Quantity = Int32.Parse(Request.Form["Quantity"]);
            current_cart.Items.Add(cart_item);
            cart_process.Edit(current_cart);
            return null;
        }

        [Authorize]
        [HttpGet]
        [Route("Shop/Checkout/")]
        public ActionResult Checkout() {
            var country_process = new CountryProcess();
            ViewBag.Countries = new SelectList(country_process.SelectList(), "Id", "Name");
            var current_session = Request.Cookies["session"];
            var current_cart = cart_process.SelectByCookie(current_session["token"].ToString());
            if (current_cart != null) {
                var current_cart_items = cart_item_process.GetAllByCartId(current_cart.Id);
                ViewBag.cart_items = JsonConvert.SerializeObject(current_cart_items);
            }
            var products = product_process.GetByCartId(current_cart.Id);
            ViewBag.cart_products = JsonConvert.SerializeObject(products);
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmarCompra() {
            Entities.Cart current_cart = cart_process.SelectByCookie(Request.Cookies["session"]["token"].ToString());
            Client cliente = NuevoCliente(Request.Form);
            client_process.Add(cliente);
            Order order = NuevoOrden(Request.Form, cliente);
            order = order_process.Add(order);
            if (current_cart != null) {
                var cart_items = cart_item_process.GetAllByCartId(current_cart.Id);
                foreach (CartItem item in cart_items) {
                    var order_detail = new OrderDetail();
                    order_detail.Price = item.Price;
                    order_detail.ProductId = item.ProductId;
                    order_detail.OrderId = order_process.FindByNumber(order.OrderNumber).Id;
                    order_detail.Quantity = item.Quantity;
                    order_detail.CreatedOn = DateTime.Now;
                    order_detail.ChangedOn = DateTime.Now;
                    order_detail_process.Add(order_detail);
                }
            }
            return Json(Url.Action("Confirmacion", "Shop"));
        }

        public ActionResult Confirmacion() {
            var current_session = Request.Cookies["session"];
            var current_cart = cart_process.SelectByCookie(current_session["token"].ToString());
            if (current_cart != null) {
                var current_cart_items = cart_item_process.GetAllByCartId(current_cart.Id);
                ViewBag.cart_items = JsonConvert.SerializeObject(current_cart_items);
            }
            var products = product_process.GetByCartId(current_cart.Id);
            ViewBag.cart_products = JsonConvert.SerializeObject(products);
            if (Request.Cookies["session"] != null) {
                Response.Cookies["session"].Expires = DateTime.Now.AddDays(-10);
                cart_process.Delete(current_cart.Id);
            }
            return View();
        }




        private Order NuevoOrden(NameValueCollection form, Client cliente) {
            var order = new Order();
            order.OrderNumber = random.Next();
            order.CreatedOn = DateTime.Now;
            order.ChangedOn = DateTime.Now;
            order.Rowid = Guid.NewGuid();
            order.OrderDate = DateTime.Now;
            order.State = "Approved";
            order.TotalPrice = Double.Parse(form["TotalPrice"]);
            order.ItemCount = int.Parse(form["ItemCount"]);
            order.ClientId = client_process.FindByUser(cliente.AspNetUsers.Replace(".", "-")).Id;
            return order;
        }

        private Client NuevoCliente(NameValueCollection form) {
            Client cliente = new Client();
            cliente.AspNetUsers = User.Identity.Name;
            cliente.Email = form["Email"];
            cliente.FirstName = form["FirstName"];
            cliente.LastName = form["LastName"];
            cliente.City = form["City"];
            cliente.Rowid = Guid.NewGuid();
            cliente.CountryId = int.Parse(form["CountryId"]);
            cliente.CreatedOn = DateTime.Now;
            cliente.ChangedOn = DateTime.Now;
            cliente.SignupDate = DateTime.Now;
            return cliente;
        }


        [HttpGet]
        [Route("Shop/ObtenerPagina")]
        public JsonResult ObtenerPagina(int pag, string pattern) {
            var products = pattern == "" ? product_process.SelectList() : product_process.GetByName(pattern);
            dynamic response = new ExpandoObject();
            response.page_count = Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(PAGE_SIZE));
            response.products = GetPage(products, pag -1, PAGE_SIZE);
            return Json(response, JsonRequestBehavior.AllowGet);
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

        private List<Product> GetPage(List<Product> list, int page, int pageSize) {
            return list.Skip(page * pageSize).Take(pageSize).ToList();
        }
    }

}