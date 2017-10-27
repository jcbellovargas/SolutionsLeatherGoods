using ASF.UI.Process;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.UI.WbSite.Services.Cache;

namespace ASF.UI.WbSite.Areas.Client.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client/Client
        public ActionResult Index()
        {
            
            var client_process = new ClientProcess();

            return View(client_process.SelectList());
        }

        public ActionResult Create()
        {
            var category_process = new CategoryProcess();
            var country_process = new CountryProcess();
            ViewBag.Categories = new SelectList(category_process.SelectList(), "Id", "Name");
            ViewBag.Countries = new SelectList(country_process.SelectList(), "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ASF.Entities.Client client)
        {
            var client_process = new ClientProcess();
            client.CreatedOn = DateTime.Now;
            client.CreatedBy = 1;
            client.ChangedBy = 1;
            client.ChangedOn = DateTime.Now;
            client.Rowid = Guid.NewGuid();
            client_process.Add(client);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var client_process = new ClientProcess();
            return View(client_process.Find(id));
        }

        public ActionResult Edit(int id)
        {
            var client_process = new ClientProcess();
            var client = client_process.Find(id);
            TempData["client"] = client;
            return View(client);
        }

        [HttpPost]
        public ActionResult Edit(ASF.Entities.Client client)
        {
            var client_process = new ClientProcess();
            ASF.Entities.Client old_client = ((ASF.Entities.Client)TempData["client"]);
            client.Id = old_client.Id;
            client.ChangedOn = DateTime.Now;
            client.Rowid = old_client.Rowid;
            client.CreatedBy = old_client.CreatedBy;
            client.CreatedOn = old_client.CreatedOn;
            client.ChangedBy = 1;
            client_process.Edit(client);
            return RedirectToAction("Details", new { id = client.Id });
        }

        public ActionResult Delete(int id)
        {
            var client_process = new ClientProcess();
            var client = client_process.Find(id);
            TempData["client"] = client;
            return View(client);
        }

        [HttpPost]
        public ActionResult Delete(ASF.Entities.Client client)
        {
            var client_process = new ClientProcess();
            client.Id = ((ASF.Entities.Client)TempData["client"]).Id;
            client_process.Delete(client.Id);
            return RedirectToAction("Index");
        }
    }
}