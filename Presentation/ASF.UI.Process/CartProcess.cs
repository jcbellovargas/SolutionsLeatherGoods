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
    public class CartProcess : ProcessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Cart> SelectList()
        {
            var response = HttpGet<AllResponse<Cart>>("rest/Cart/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Add(Cart c)
        {
            HttpPost<Cart>("rest/Cart/Add", c, MediaType.Json);
        }

        public Cart Find(int id)
        {
            var response = HttpGet<FindResponse<Cart>>("rest/Cart/Find/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Edit(Cart c)
        {
            HttpPost<Cart>("rest/Cart/Edit", c, MediaType.Json);
        }

        public void Delete(int id)
        {
            HttpGet<FindResponse<Cart>>("rest/Cart/Remove/" + id, new Dictionary<string, object>(), MediaType.Json);
        }
    }
}