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
    public class CountryProcess : ProcessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> SelectList()
        {
            var response = HttpGet<AllResponse<Country>>("rest/Country/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Add(Country c)
        {
            HttpPost<Country>("rest/Country/Add", c, MediaType.Json);
        }

        public Country Find(int id)
        {
            var response = HttpGet<FindResponse<Country>>("rest/Country/Find/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Edit(Country c)
        {
            HttpPost<Country>("rest/Country/Edit", c, MediaType.Json);
        }

        public void Delete(int id)
        {
            HttpGet<FindResponse<Country>>("rest/Country/Remove/" + id, new Dictionary<string, object>(), MediaType.Json);
        }

        public List<Country> GetByPattern(string term) {
            var response = HttpGet<AllResponse<Country>>("rest/Country/GetByPattern/" + term, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }
    }
}