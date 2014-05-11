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
    public class RoundController : Controller
    {
        TimetablingEntities db = new TimetablingEntities();
        //
        // GET: /Round/

        public ActionResult Index()
        {
            RoundModel model = new RoundModel();
            model.rounds = new Round();
            IEnumerable<SelectListItem> selectList = from s in db.Semesters
                                                     select new SelectListItem
                                                     {
                                                         Value = (s.SemesterID).ToString(),
                                                         Text = (s.StartYear).ToString() + " - " +
                                                         (s.StartYear + 1).ToString() +
                                                         " Semester " + (s.SemesterNo).ToString()

                                                     };
            ViewBag.academicDates = new SelectList(selectList, "Value", "Text");


            IEnumerable<SelectListItem> currentRoundList = from s in db.Semesters
                                                           where s.IsActive == true
                                                           select new SelectListItem

                                                           {
                                                               Value = (s.SemesterID).ToString(),
                                                               Text = (s.StartYear).ToString() + " - " + (s.StartYear + 1)
                                                               + " Semester " + (s.SemesterNo).ToString()

                                                           };
            model.currentRounds = new SelectList(currentRoundList, "Value", "Text");

            IEnumerable<SelectListItem> nonActiveSemesterlist = from s in db.Semesters
                                                               where s.IsActive == false
                                                               select new SelectListItem

                                                               {
                                                                   Value = (s.SemesterID).ToString(),
                                                                   Text = (s.StartYear).ToString() + " - " + (s.StartYear + 1)
                                                                   + " Semester " + (s.SemesterNo).ToString()

                                                               };
            model.nonActiveSemesters = new SelectList(nonActiveSemesterlist, "Value", "Text");





            return View(model);
        }
        [HttpPost]
        public ActionResult Index(int i)
        {

            return View();
        }
    }
}
