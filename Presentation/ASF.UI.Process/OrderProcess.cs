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
    public class OrderProcess : ProcessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Order> SelectList()
        {
            var response = HttpGet<AllResponse<Order>>("rest/Order/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Add(Order c)
        {
            HttpPost<Order>("rest/Order/Add", c, MediaType.Json);
        }

        public Order Find(int id)
        {
            var response = HttpGet<FindResponse<Order>>("rest/Order/Find/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Edit(Order c)
        {
            HttpPost<Order>("rest/Order/Edit", c, MediaType.Json);
        }

        public void Delete(int id)
        {
            HttpGet<FindResponse<Order>>("rest/Order/Remove/" + id, new Dictionary<string, object>(), MediaType.Json);
        }
    }
}