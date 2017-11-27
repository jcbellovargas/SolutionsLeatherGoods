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
    public class CategoryProcess : ProcessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Category> SelectList()
        {
            var response = HttpGet<AllResponse<Category>>("rest/Category/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Add(Category c) {
            HttpPost<Category>("rest/Category/Add", c, MediaType.Json);
        }

        public Category Find(int id)
        {
            var response = HttpGet<FindResponse<Category>>("rest/Category/Find/"+id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Edit(Category c)
        {
            HttpPost<Category>("rest/Category/Edit", c, MediaType.Json);
        }

        public void Delete(int id)
        {
            HttpGet<FindResponse<Category>>("rest/Category/Remove/" + id, new Dictionary<string, object>(), MediaType.Json);
        }

        public List<Category> GetByPattern(string term) {
            var response = HttpGet<AllResponse<Category>>("rest/Category/GetByPattern/" + term, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }
    }
}