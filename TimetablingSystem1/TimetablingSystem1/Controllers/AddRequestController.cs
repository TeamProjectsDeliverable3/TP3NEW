using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimetablingSystem1.Models;

namespace TimetablingSystem1.Controllers
{
    public class AddRequestController : Controller
    {
        TimetablingEntities db = new TimetablingEntities();


        public ActionResult Index()
        {
            string department = Session["department"] as string;
            IEnumerable<SelectListItem> selectList = from s in db.Modules
                                                     where s.DepartmentCode == department
                                                     select new SelectListItem
                                                     {
                                                         Text = s.ModuleCode + " - " + s.Title,
                                                         Value = s.ModuleCode

                                                     };

            AddRequestModel model = new AddRequestModel();
            ViewBag.modules = new SelectList(selectList, "Value", "Text");
            return View();
        }

    }
}
