using ASF.UI.Process;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.UI.WbSite.Services.Cache;

namespace ASF.UI.WbSite.Areas.Country.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country/Country
        public ActionResult Index()
        {
            
            var country_process = new CountryProcess();
            return View(country_process.SelectList());
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ASF.Entities.Country c)
        {
            var country_process = new CountryProcess();
            country_process.Add(c);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var country_process = new CountryProcess();
            return View(country_process.Find(id));
        }

        public ActionResult Edit(int id)
        {
            var country_process = new CountryProcess();
            var country = country_process.Find(id);
            TempData["country"] = country;
            return View(country);
        }

        [HttpPost]
        public ActionResult Edit(ASF.Entities.Country c)
        {
            var country_process = new CountryProcess();
            var old_country = ((ASF.Entities.Country)TempData["country"]);
            c.Id = old_country.Id;
            c.ChangedBy = old_country.ChangedBy;
            c.ChangedOn = old_country.ChangedOn;
            c.CreatedOn = old_country.ChangedOn;
            c.CreatedBy = old_country.CreatedBy;
            country_process.Edit(c);
            return RedirectToAction("Details", new { id = c.Id });
        }

        public ActionResult Delete(int id)
        {
            var country_process = new CountryProcess();
            var country = country_process.Find(id);
            TempData["country"] = country;
            return View(country);
        }

        [HttpPost]
        public ActionResult Delete(ASF.Entities.Country c)
        {
            var country_process = new CountryProcess();
            c.Id = ((ASF.Entities.Country)TempData["country"]).Id;
            country_process.Delete(c.Id);
            return RedirectToAction("Index");
        }
    }
}