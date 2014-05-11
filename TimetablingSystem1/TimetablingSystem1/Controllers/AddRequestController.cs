using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            AddRequestModel model = new AddRequestModel();

            string department = User.Identity.Name;

            
            
            IEnumerable<SelectListItem> selectList = from s in db.Modules
                                                     where s.DepartmentCode == department
                                                     select new SelectListItem
                                                     {
                                                         Text = s.ModuleCode + " - " + s.Title,
                                                         Value = s.ModuleCode

                                                     };

            ViewBag.modules = new SelectList(selectList, "Value", "Text");

            IEnumerable<SelectListItem> selectActiveRoundList = from r in db.Rounds join  s in db.Semesters on r.SemesterID equals s.SemesterID
                                                                where r.EndDate == null
                                                                select new SelectListItem {

                                                                    Text = (s.StartYear).ToString() + " - " + (s.StartYear+1).ToString() + 
                                                                    " Semester " + (s.SemesterNo).ToString() ,

                                                                    Value = (r.RoundID).ToString()

                                                                };
            ViewBag.activeSemesters = new SelectList(selectActiveRoundList, "Value", "Text");

            IEnumerable<SelectListItem> daysList = new List<SelectListItem>()
                                                   {
                new SelectListItem() {Text="Monday", Value="1"},
                new SelectListItem() {Text="Tuesday", Value="2"},
                new SelectListItem() {Text="Wednesday", Value="3"},
                new SelectListItem() {Text="Thursday", Value="4"},
                new SelectListItem() {Text="Friday", Value="5"},

                                                   };

            ViewBag.days = new SelectList(daysList, "Value", "Text");

            IEnumerable<SelectListItem> Periods = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Period 1 - 9:00", Value="1"},
                new SelectListItem() {Text="Period 2 - 10:00", Value="2"},
                new SelectListItem() {Text="Period 3 - 11:00", Value="3"},
                new SelectListItem() {Text="Period 4 - 12:00", Value="4"},
                new SelectListItem() {Text="Period 5 - 13:00", Value="5"},
                new SelectListItem() {Text="Period 6 - 14:00", Value="6"},
                new SelectListItem() {Text="Period 7 - 15:00", Value="7"},
                new SelectListItem() {Text="Period 8 - 16:00", Value="8"},
                new SelectListItem() {Text="Period 9 - 17:00", Value="9"}
            
            };

            ViewBag.periods = new SelectList(Periods, "Value", "Text");

            IEnumerable<SelectListItem> roomsList = new List<SelectListItem>()
            {
               
                new SelectListItem() {Text="1", Value="1"},
                new SelectListItem() {Text="2", Value="2"},
                new SelectListItem() {Text="3", Value="3"},
                new SelectListItem() {Text="4", Value="4"},
                new SelectListItem() {Text="5", Value="5"},
         
            
            };

            ViewBag.noOfRooms = new SelectList(roomsList, "Value", "Text");

            IEnumerable<SelectListItem> parkCodesList = new List<SelectListItem>()
            {
               
                new SelectListItem() {Text="W - West", Value="W"},
                new SelectListItem() {Text="C - Central", Value="C"},
                new SelectListItem() {Text="E - East", Value="E"},
               
         
            
            };

            ViewBag.parkCodes = new SelectList(parkCodesList, "Value", "Text");

            model.facilities = (from f in db.Facilities select f).ToList();
            ViewBag.noOfFacs = (from f in db.Facilities select f.FacilityID).Count();

            



            return View(model);




        }

        [HttpPost]
        public ActionResult Index(AddRequestModel model, HttpPostedFileBase LogoFile)
        {
            
            

            string department = User.Identity.Name;
            IEnumerable<SelectListItem> selectList = from s in db.Modules
                                                     where s.DepartmentCode == department
                                                     select new SelectListItem
                                                     {
                                                         Text = s.ModuleCode + " - " + s.Title,
                                                         Value = s.ModuleCode

                                                     };

           
            ViewBag.modules = new SelectList(selectList, "Value", "Text");

            IEnumerable<SelectListItem> selectActiveRoundList = from r in db.Rounds
                                                                join s in db.Semesters on r.SemesterID equals s.SemesterID
                                                                where r.EndDate == null
                                                                select new SelectListItem
                                                                {

                                                                    Text = (s.StartYear).ToString() + " - " + (s.StartYear + 1).ToString() +
                                                                    " Semester " + (s.SemesterNo).ToString(),

                                                                    Value = (r.RoundID).ToString()

                                                                };
            ViewBag.activeSemesters = new SelectList(selectActiveRoundList, "Value", "Text");

            IEnumerable<SelectListItem> daysList = new List<SelectListItem>()
                                                   {
                new SelectListItem() {Text="Monday", Value="1"},
                new SelectListItem() {Text="Tuesday", Value="2"},
                new SelectListItem() {Text="Wednesday", Value="3"},
                new SelectListItem() {Text="Thursday", Value="4"},
                new SelectListItem() {Text="Friday", Value="5"},

                                                   };

            ViewBag.days = new SelectList(daysList, "Value", "Text");

            IEnumerable<SelectListItem> Periods = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Period 1 - 9:00", Value="1"},
                new SelectListItem() {Text="Period 2 - 10:00", Value="2"},
                new SelectListItem() {Text="Period 3 - 11:00", Value="3"},
                new SelectListItem() {Text="Period 4 - 12:00", Value="4"},
                new SelectListItem() {Text="Period 5 - 13:00", Value="5"},
                new SelectListItem() {Text="Period 6 - 14:00", Value="6"},
                new SelectListItem() {Text="Period 7 - 15:00", Value="7"},
                new SelectListItem() {Text="Period 8 - 16:00", Value="8"},
                new SelectListItem() {Text="Period 9 - 17:00", Value="9"}
            
            };

            ViewBag.periods = new SelectList(Periods, "Value", "Text");

            IEnumerable<SelectListItem> roomsList = new List<SelectListItem>()
            {
               
                new SelectListItem() {Text="1", Value="1"},
                new SelectListItem() {Text="2", Value="2"},
                new SelectListItem() {Text="3", Value="3"},
                new SelectListItem() {Text="4", Value="4"},
                new SelectListItem() {Text="5", Value="5"},
         
            
            };

            ViewBag.noOfRooms = new SelectList(roomsList, "Value", "Text");

            IEnumerable<SelectListItem> parkCodesList = new List<SelectListItem>()
            {
               
                new SelectListItem() {Text="W - West", Value="W"},
                new SelectListItem() {Text="C - Central", Value="C"},
                new SelectListItem() {Text="E - East", Value="E"},
               
         
            
            };

            ViewBag.parkCodes = new SelectList(parkCodesList, "Value", "Text");
            ViewBag.noOfFacs = (from f in db.Facilities select f.FacilityID).Count();
            model.facilities = (from f in db.Facilities select f).ToList();
            

            if (ModelState.IsValid)
            if(model.requestsIndex != null){
            {
                int prefrooms = Convert.ToInt32(Request["roomSelect"]);
                Request requests = new Request();

                model.requestsIndex.RoundID = Convert.ToInt32(Request["roundSelect"]);
                model.requestsIndex.ModuleCode = Request["moduleSelect"];
                model.requestsIndex.Day = Convert.ToByte(Request["daySelect"]);
                model.requestsIndex.Period = Convert.ToByte(Request["periodSelect"]);
                model.requestsIndex.Length = Convert.ToByte(Request["lengthData"]);
                model.requestsIndex.NoOfRooms = Convert.ToByte(Request["roomSelect"]);
                model.requestsIndex.NoOfStudents = Convert.ToInt32(Request["noOfStudents"]);
                model.requestsIndex.RoomType = Request["roomType"];
                model.requestsIndex.SeatingType = Request["seatingType"];
                model.requestsIndex.ParkCode = Request["parkSelect"];

                model.requestsIndex.Status = "P";
                if (Request["priorityCheck"] == "1")
                {
                    model.requestsIndex.Priority = 1;
                }
                else { model.requestsIndex.Priority = 0; }
                requests = model.requestsIndex;

                db.Entry(requests).State = EntityState.Added;
                db.SaveChanges();

               

                for (int i = 1; i < 16; i++)
                {

                    if (Request["week-" + i] == "1" )
                    {

                      RequestWeek requestweeks = new RequestWeek();
                      requestweeks.RequestID = requests.RequestID;
                      requestweeks.Week = i;
                      db.Entry(requestweeks).State = EntityState.Added;
                      db.SaveChanges();

                  }
                    

                }

                for (int i = 1; i <= prefrooms; i++)
                {

                    PreferenceRoom preferenceRooms = new PreferenceRoom();
                    preferenceRooms.RequestID = requests.RequestID;
                    preferenceRooms.RoomID = Convert.ToInt32(Request["roomPref-" + i]);
                    db.Entry(preferenceRooms).State = EntityState.Added;
                    db.SaveChanges();


                }

                for (int i = 1; i <= ViewBag.noOfFacs; i++) {

                    RequiredFacility requiredFacilities = new RequiredFacility();
                    requiredFacilities.RequestID = requests.RequestID;

                    string fac = Request["fac-"+i];
                    
                    if ( fac == "1") {

                        requiredFacilities.FacilityID = i;
                        db.Entry(requiredFacilities).State = EntityState.Added;
                        db.SaveChanges();

                    }
                
                
                
                }


                    return RedirectToAction("Index", "ViewRequest");
            }
          }

            return View("Index");
        }

    }
}
