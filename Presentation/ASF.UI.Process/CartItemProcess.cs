using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using ASF.Entities;
using ASF.Services.Contracts;
using ASF.UI.Process;

namespace ASF.UI.Process
{
    public class CartItemProcess : ProcessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CartItem> SelectList()
        {
            var response = HttpGet<AllResponse<CartItem>>("rest/CartItem/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Add(CartItem c)
        {
            HttpPost<CartItem>("rest/CartItem/Add", c, MediaType.Json);
        }

        public CartItem Find(int id)
        {
            var response = HttpGet<FindResponse<CartItem>>("rest/CartItem/Find/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Edit(CartItem c)
        {
            HttpPost<CartItem>("rest/CartItem/Edit", c, MediaType.Json);
        }

        public void Delete(int id)
        {
            HttpGet<FindResponse<CartItem>>("rest/CartItem/Remove/" + id, new Dictionary<string, object>(), MediaType.Json);
        }

        public List<CartItem> GetAllByCartId(int id) {
            var response = HttpGet<AllResponse<CartItem>>("rest/CartItem/GetAllByCartId/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }
    }
}