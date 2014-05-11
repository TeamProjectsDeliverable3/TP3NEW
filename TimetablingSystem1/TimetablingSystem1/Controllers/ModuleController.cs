using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimetablingSystem1.Models;

namespace TimetablingSystem1.Controllers
{
    public class ModuleController : Controller
    {
        TimetablingEntities db = new TimetablingEntities();
        //
        // GET: /Module/

        public ActionResult Index()
        {
            string department = User.Identity.Name;
            ModuleModel model = new ModuleModel();
            model.modulesIndex = (from s in db.Modules where s.DepartmentCode == department select s).ToList();

            return View(model);
        }

        //
        // GET: /Module/Create

        [HttpGet]
        public ActionResult Create()
        {
            string department = User.Identity.Name;
            ViewBag.DepartmentCode = department;
            return View();
        }

        //
        // POST: /Module/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Module modules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modules).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modules);
        }

        // GET: /Module/Edit/5

        [HttpGet]
        public ActionResult Edit(string id = "")
        {
            ModuleModel mod = new ModuleModel();
            mod.modulesEdit = db.Modules.Find(id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(mod);
        }

        // POST: /Module/Edit/5

        [HttpPost]
        public ActionResult Edit(ModuleModel model)
        {
            Module mod = new Module();
            mod.ModuleCode = model.ModuleCode;
            mod.Title = model.Title;
            mod.DepartmentCode = model.DepartmentCode;

            if (ModelState.IsValid)
            {
                db.Entry(mod).State = EntityState.Modified; ;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /Module/Delete/5
        [HttpGet]
        public ActionResult Delete(string id = "")
        {
            Module modules = db.Modules.Find(id);
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(modules);
        }

        // POST: /Module/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Module modules = db.Modules.Find(id);
            db.Modules.Remove(modules);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}