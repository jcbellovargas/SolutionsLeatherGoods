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
    public class DealerProcess : ProcessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Dealer> SelectList()
        {
            var response = HttpGet<AllResponse<Dealer>>("rest/Dealer/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Add(Dealer c)
        {
            HttpPost<Dealer>("rest/Dealer/Add", c, MediaType.Json);
        }

        public Dealer Find(int id)
        {
            var response = HttpGet<FindResponse<Dealer>>("rest/Dealer/Find/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Edit(Dealer c)
        {
            HttpPost<Dealer>("rest/Dealer/Edit", c, MediaType.Json);
        }

        public void Delete(int id)
        {
            HttpGet<FindResponse<Dealer>>("rest/Dealer/Remove/" + id, new Dictionary<string, object>(), MediaType.Json);
        }
    }
}