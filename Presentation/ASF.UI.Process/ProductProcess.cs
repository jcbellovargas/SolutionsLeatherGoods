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
    public class ProductProcess : ProcessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Product> SelectList()
        {
            var response = HttpGet<AllResponse<Product>>("rest/Product/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public List<Product> GetByName(string name) {
            var response = HttpGet<AllResponse<Product>>("rest/Product/GetByName/" + name, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Add(Product c)
        {
            HttpPost<Product>("rest/Product/Add", c, MediaType.Json);
        }

        public Product Find(int id)
        {
            var response = HttpGet<FindResponse<Product>>("rest/Product/Find/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Edit(Product c)
        {
            HttpPost<Product>("rest/Product/Edit", c, MediaType.Json);
        }

        public void Delete(int id)
        {
            HttpGet<FindResponse<Product>>("rest/Product/Remove/" + id, new Dictionary<string, object>(), MediaType.Json);
        }

        public object GetByCartId(int id) {
            var response = HttpGet<AllResponse<Product>>("rest/Product/GetByCartId/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }
    }
}