using ASF.UI.Process;
using ASF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASF.UI.WbSite.Services.Cache;

namespace ASF.UI.WbSite.Areas.Category.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category/Category
        public ActionResult Index()
        {
            
            var category_process = new CategoryProcess();
            return View(category_process.SelectList());
            //return View(DataCache.Instance.CategoryAll());
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ASF.Entities.Category c)
        {
            var category_process = new CategoryProcess();
            category_process.Add(c);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var category_process = new CategoryProcess();
            return View(category_process.Find(id));
        }

        public ActionResult Edit(int id)
        {
            var category_process = new CategoryProcess();
            var category = category_process.Find(id);
            TempData["category"] = category;
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(ASF.Entities.Category c)
        {
            var category_process = new CategoryProcess();
            c.Id = ((ASF.Entities.Category)TempData["category"]).Id;
            category_process.Edit(c);
            return RedirectToAction("Details", new { id = c.Id });
        }

        public ActionResult Delete(int id)
        {
            var category_process = new CategoryProcess();
            var category = category_process.Find(id);
            TempData["category"] = category;
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(ASF.Entities.Category c)
        {
            var category_process = new CategoryProcess();
            c.Id = ((ASF.Entities.Category)TempData["category"]).Id;
            category_process.Delete(c.Id);
            return RedirectToAction("Index");
        }
    }
}