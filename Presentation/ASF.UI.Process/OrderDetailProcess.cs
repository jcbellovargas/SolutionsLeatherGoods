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
    public class OrderDetailProcess : ProcessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderDetail> SelectList()
        {
            var response = HttpGet<AllResponse<OrderDetail>>("rest/OrderDetail/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Add(OrderDetail c)
        {
            HttpPost<OrderDetail>("rest/OrderDetail/Add", c, MediaType.Json);
        }

        public OrderDetail Find(int id)
        {
            var response = HttpGet<FindResponse<OrderDetail>>("rest/OrderDetail/Find/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Edit(OrderDetail c)
        {
            HttpPost<OrderDetail>("rest/OrderDetail/Edit", c, MediaType.Json);
        }

        public void Delete(int id)
        {
            HttpGet<FindResponse<OrderDetail>>("rest/OrderDetail/Remove/" + id, new Dictionary<string, object>(), MediaType.Json);
        }
    }
}