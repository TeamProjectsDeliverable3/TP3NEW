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
    public class ViewRequestController : Controller
    {

        TimetablingEntities db = new TimetablingEntities();
        //
        // GET: /ViewRequest/

        public ViewRequestModel getRequests(Request request) {

            string department = User.Identity.Name;
            //List<Request> requests = new List<Request>();
            //List<Module> modules = new List<Module>();
            ViewRequestModel model = new ViewRequestModel();

            model.moduleCode = request.ModuleCode;
           // model.days = request.Day;
           // model.startTime = request.Period;
          //  model.endTime = request.Length;
            model.numberOfRooms = request.NoOfRooms;
            model.priority = request.Priority;

            model.moduleTitle = db.Modules.Where(m => m.ModuleCode == request.ModuleCode).First().Title;

            string weekDay = "";
            switch (int.Parse(request.Day.ToString()))
            {
                case 1:
                    weekDay = "Mon";
                    break;
                case 2:
                    weekDay = "Tues";
                    break;
                case 3:
                    weekDay = "Wed";
                    break;
                case 4:
                    weekDay = "Thur";
                    break;
                case 5:
                    weekDay = "Fri";
                    break;
                case 6:
                    weekDay = "Sat";
                    break;
                case 7:
                    weekDay = "Sun";
                    break;
                default:
                    weekDay = "Unknown";
                    break;
            }

            model.days = weekDay;

            string startTime = "";
            switch (int.Parse(request.Period.ToString()))
            {
                case 1:
                    startTime = "9:00";
                    break;
                case 2:
                    startTime = "10:00";
                    break;
                case 3:
                    startTime = "11:00";
                    break;
                case 4:
                    startTime = "12:00";
                    break;
                case 5:
                    startTime = "13:00";
                    break;
                case 6:
                    startTime = "14:00";
                    break;
                case 7:
                    startTime = "15:00";
                    break;
                case 8:
                    startTime = "16:00";
                    break;
                case 9:
                    startTime = "17:00";
                    break;
                default:
                    startTime = "Unknown";
                    break;
            }
            model.startTime = startTime;

            string endTime = "";
            int time = Convert.ToInt32(request.Period);
            int length = Convert.ToInt32(request.Length);
            int timelength = time + length; 

            switch (timelength)
            {
                case 2:
                    endTime = "9:50";
                    break;
                case 3:
                    endTime = "10:50";
                    break;
                case 4:
                    endTime = "11:50";
                    break;
                case 5:
                    endTime = "12:50";
                    break;
                case 6:
                    endTime = "13:50";
                    break;
                case 7:
                    endTime = "14:50";
                    break;
                case 8:
                    endTime = "15:50";
                    break;
                case 9:
                    endTime = "16:50";
                    break;
                case 10:
                    endTime = "17:50";
                    break;
                default:
                    endTime = "Unknown";
                    break;
            }

            model.endTime = endTime;

            string weeks = "";
            List<RequestWeek> reqweeks = new List<RequestWeek>();

            reqweeks = (from w in db.RequestWeeks where w.RequestID == request.RequestID select w).ToList();


            foreach (RequestWeek r in reqweeks) {

                weeks += r.Week.ToString() + ", ";
                
            
            }

            model.weeks = weeks;

            


            return model;


        
        
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<ViewRequestModel> model = new List<ViewRequestModel>();
            List<Request> requests = new List<Request>();
            string department = User.Identity.Name;

            requests = (from r in db.Requests
                        join m in db.Modules on r.ModuleCode equals m.ModuleCode
                        where m.DepartmentCode == department
                        select r).ToList();

            foreach (Request r in requests) {

                model.Add(getRequests(r));
            
            }


            return View(model);

        }

        [HttpPost]
        public ActionResult Index(ViewRequestModel model)
        {
            

            string department = User.Identity.Name;

         //   model.requestIndex = (from r in db.Requests where r.ModuleCode.Substring(0, 2) == department select r).ToList();

            return View();

        }

    }
}
