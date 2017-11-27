﻿using System;
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
    public class ClientProcess : ProcessComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Client> SelectList()
        {
            var response = HttpGet<AllResponse<Client>>("rest/Client/All", new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public Client Add(Client c)
        {
            return HttpPost<Client>("rest/Client/Add", c, MediaType.Json);
        }

        public Client Find(int id)
        {
            var response = HttpGet<FindResponse<Client>>("rest/Client/Find/" + id, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public Client FindByGuid(Guid guid) {
            var response = HttpGet<FindResponse<Client>>("rest/Client/FindByGuid/" + guid, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public void Edit(Client c)
        {
            HttpPost<Client>("rest/Client/Edit", c, MediaType.Json);
        }

        public void Delete(int id)
        {
            HttpGet<FindResponse<Client>>("rest/Client/Remove/" + id, new Dictionary<string, object>(), MediaType.Json);
        }

        public Client FindByName(string name) {
            var response = HttpGet<FindResponse<Client>>("rest/Client/FindByName/" + name, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }

        public Client FindByUser(string aspNetUsers) {
            var response = HttpGet<FindResponse<Client>>("rest/Client/FindByUser/" + aspNetUsers, new Dictionary<string, object>(), MediaType.Json);
            return response.Result;
        }
    }
}