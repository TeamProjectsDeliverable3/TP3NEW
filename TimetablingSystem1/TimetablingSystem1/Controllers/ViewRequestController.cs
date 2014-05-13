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
          
            ViewRequestModel model = new ViewRequestModel();

            model.moduleCode = request.ModuleCode;
       
            model.numberOfRooms = request.NoOfRooms;
            model.priority = request.Priority;
            model.requestID = request.RequestID;
           

            model.moduleTitle = db.Modules.Where(m => m.ModuleCode == request.ModuleCode).First().Title;

            

            string requestStatus = request.Status;
            string status = "";

            switch(requestStatus){
                case "P":
                    status = "Pending";
                    break;
                case "A":
                    status = "Approved";
                    break;
                case "R":
                    status = "Pending";
                    break;
                default:
                    status = "";
                    break;

            }
            model.status = status;
            model.statusCode = requestStatus;

            string weekDay = "";
            int days = int.Parse(request.Day.ToString());
            switch (days)
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
            model.daysint = days;

            string startTime = "";
            int startint = int.Parse(request.Period.ToString());
            switch (startint)
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
            model.startint = startint;

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
            model.endint = timelength;

            string weeks = "";
            List<RequestWeek> reqweeks = new List<RequestWeek>();

            reqweeks = (from w in db.RequestWeeks where w.RequestID == request.RequestID select w).ToList();


            foreach (RequestWeek r in reqweeks) {

                weeks += r.Week.ToString() + ", ";
                
            
            }

            model.weeks = weeks;

            


            return model;


        
        
        }


        public ActionResult Index(string sortOrder, string searchString)
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

            ViewBag.CodeSortParm = String.IsNullOrEmpty(sortOrder) ? "mod" : "moddesc" ;
            ViewBag.TitleSortParm = sortOrder ==  "title" ? "titledesc": "title";
            ViewBag.DaySortParm = sortOrder == "day" ? "daydesc": "day";
            ViewBag.StartSortParm = sortOrder == "start" ? "startdesc" : "start";
            ViewBag.EndSortParm = sortOrder == "end" ? "enddesc" : "end";

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(o => o.moduleCode.ToUpper().Contains(searchString.ToUpper())
                                    || o.moduleTitle.ToUpper().Contains(searchString.ToUpper())
                    
                    ).ToList();
                                       
                                       
            }



          

              switch (sortOrder)
              {
                  case "moddesc":
                      model = model.OrderByDescending(o => o.moduleCode).ToList();
                      break;
                  case "mod":
                      model = model.OrderBy(o => o.moduleCode).ToList();
                      break;
                 case "titledesc":
                     model = model.OrderByDescending(o => o.moduleTitle).ToList();
                      break;
                  case "title":
                     model = model.OrderBy(o => o.moduleTitle).ToList();
                      break;
                  case "daydesc":
                      model = model.OrderByDescending(o => o.daysint).ToList();
                      break;
                  case "day":
                      model = model.OrderBy(o => o.daysint).ToList();
                      break;
                  case "start":
                      model = model.OrderBy(o => o.startint).ToList();
                      break;
                  case "startdesc":
                      model = model.OrderByDescending(o => o.startint).ToList();
                      break;
                  case "end":
                      model = model.OrderBy(o => o.endint).ToList();
                      break;
                  case "enddesc":
                      model = model.OrderByDescending(o => o.endint).ToList();
                      break;
                  default:
                      model = model.OrderBy(o => o.moduleCode).ToList();
                      break;
              }

              if (model != null)
              {
                  return View(model);
              }

              return RedirectToAction("Index", "ViewRequest");

        }

        public ActionResult Delete(int? id)
        {

            Request req = db.Requests.Find(id);

            return View(req);
        } 



       /* [HttpGet]
        public ActionResult Delete(int reqID) {

            ViewRequestModel model = new ViewRequestModel();
            Request req = db.Requests.Find(reqID);
            model.requestDelete = req;

            return View(model);
        
        }  */

       
        [HttpPost]
        public ActionResult Delete(short id) {


            int reqID = id;
            
            //List<RequiredFacility> reqf = new List<RequiredFacility>();
            IQueryable<RequiredFacility> reqf = db.RequiredFacilities.Where(f => f.RequestID == reqID); //(from f in db.RequiredFacilities where f.RequestID == reqID select f);
            //List<PreferenceRoom> prefr = new List<PreferenceRoom>();
            IQueryable<PreferenceRoom> prefr = db.PreferenceRooms.Where(f => f.RequestID == reqID); //(from p in db.PreferenceRooms where p.RequestID == reqID select p);
            //List<RequestWeek> reqw = new List<RequestWeek>();
            IQueryable<RequestWeek> reqw = db.RequestWeeks.Where(f => f.RequestID == reqID); //(from r in db.RequestWeeks where r.RequestID == reqID select r);
            //List<AllocatedRoom> aroom = new List<AllocatedRoom>();
            IQueryable<AllocatedRoom> aroom = db.AllocatedRooms.Where(f => f.RequestID == reqID); //(from a in db.AllocatedRooms where a.RequestID == reqID select a);
            IQueryable<Request> reqr = db.Requests.Where(f => f.RequestID == reqID); //(from r in db.Requests where r.RequestID == reqID select r);

            //if (reqf != null) {

                foreach (RequiredFacility r in reqf) {

                    db.RequiredFacilities.Remove(r);
                    //db.SaveChanges();
                
                }
            
           // }

            //if (prefr != null)
            //{

                foreach (PreferenceRoom f in prefr)
                {

                    //db.Entry(f).State = EntityState.Deleted;
                    db.PreferenceRooms.Remove(f);
                    //db.SaveChanges();

                }

           // }

            //if (reqw != null)
            //{

                foreach (RequestWeek w in reqw)
                {

                    //db.Entry(w).State = EntityState.Deleted;
                    db.RequestWeeks.Remove(w);
                    //db.SaveChanges();

                }

            //}

            //if (aroom != null)
            //{

                foreach (AllocatedRoom a in aroom)
                {

                    //db.Entry(a).State = EntityState.Deleted;
                    db.AllocatedRooms.Remove(a);
                    //db.SaveChanges();

                }

            //}



            //if (id != null) {

                foreach (Request a in reqr)
                {

                    //db.Entry(a).State = EntityState.Deleted;
                    db.Requests.Remove(a);
                    //db.SaveChanges();

                }
                //db.Entry(req).State = EntityState.Deleted;
            
            
            //}

            db.SaveChanges();


            return RedirectToAction("Index", "ViewRequest");
        
        
        
        } 

      

    }
}
