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
    public class RoomLookupController : Controller
    {
        //
        // GET: /RoomLookup/
        TimetablingEntities db = new TimetablingEntities();

        public bool isChecked(int roomFac, IEnumerable<RoomFacility> facility)
        {

            foreach (var item in facility)
            {
                if (roomFac == item.FacilityID)
                {
                    return true;
                }

            }

            return false;

        }

        

        public ActionResult Index()
        {
            RoomLookupModel model = new RoomLookupModel();
            model.roomsIndex = (from s in db.Rooms select s).ToList();
            model.building = new Building();
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        
        {
            
            RoomLookupModel model = new RoomLookupModel();
            model.roomsEdit = db.Rooms.Find(id);
            model.facilityEdit = (from f in db.Facilities select f).ToList();
           // model.facilityIsChecked = true;

            List<int> roomFac = (from i in db.RoomFacilities where i.RoomID == id select i.FacilityID).ToList();
            
           
            
            

         //  model.facilityIsChecked = roomFac.Contains(ViewBag.roomfacs);
                        

            IEnumerable<SelectListItem> selectList = from b in db.Buildings 
                                                     select new SelectListItem
                                                     {
                                                         Value = b.BuildingCode,
                                                         Text = b.BuildingCode + " - " + b.Name

                                                     };
            ViewBag.EditDepartments = new SelectList(selectList, "Value", "Text");

        
            
            
            
            Room rooms = db.Rooms.Find(id);
            
            if (rooms == null)
            {

                return HttpNotFound();

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RoomLookupModel model)
        {
            Room rooms = new Room();
            rooms = model.roomsEdit;

            if (ModelState.IsValid)
            {
                if (rooms != null)
                {
                    try
                    {
                      

                        //db.Rooms.Attach(rooms);
                        db.Entry(rooms).State = EntityState.Modified;
                        //db.SaveChanges();
                        
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }

                    catch (DbEntityValidationException dbEx)
                    {
                        return RedirectToAction("Edit");
                    }


                }

                
            }
            return View(rooms);
        }

        public ActionResult AddBuilding()
        {

            return View();

        }

        [HttpPost]
        public ActionResult AddBuilding(Building buildings) {

            Room rooms = new Room();
            if (ModelState.IsValid)
            {

               
                //db.Entry(buildings).State = EntityState.Added;
                //db.SaveChanges();
                //return View("Index");
            }

            return View("Edit");
        }

    }
}
