

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



        public ActionResult Index(String sortOrder, string searchString)
        {

            //RoomLookupModel model = new RoomLookupModel();

            //model.building = new Building();

            //return View(model);

            //------------------

            RoomLookupModel model = new RoomLookupModel();
            model.roomsIndex = (from s in db.Rooms select s).ToList();

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.BCodeSortParm = sortOrder == "bcode" ? "bcode_desc" : "bcode";
            ViewBag.RTypeSortParm = sortOrder == "rtype" ? "rtype_desc" : "rtype";
            ViewBag.STypeSortParm = sortOrder == "stype" ? "stype_desc" : "stype";




            if (!String.IsNullOrEmpty(searchString))
            {
                model.roomsIndex = model.roomsIndex.Where(o => o.Name.ToUpper().Contains(searchString.ToUpper())
                                       || o.BuildingCode.ToUpper().Contains(searchString.ToUpper())
                                       );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    model.roomsIndex = model.roomsIndex.OrderByDescending(o => o.Name);
                    break;
                case "bcode":
                    model.roomsIndex = model.roomsIndex.OrderBy(o => o.BuildingCode);
                    break;
                case "bcode_desc":
                    model.roomsIndex = model.roomsIndex.OrderByDescending(o => o.BuildingCode);
                    break;
                case "rtype":
                    model.roomsIndex = model.roomsIndex.OrderBy(o => o.RoomType);
                    break;
                case "rtype_desc":
                    model.roomsIndex = model.roomsIndex.OrderByDescending(o => o.RoomType);
                    break;
                case "stype":
                    model.roomsIndex = model.roomsIndex.OrderBy(o => o.SeatingType);
                    break;
                case "stype_desc":
                    model.roomsIndex = model.roomsIndex.OrderByDescending(o => o.SeatingType);
                    break;
                default:
                    model.roomsIndex = model.roomsIndex.OrderBy(o => o.Name);
                    break;
            }

            return View(model);

        }
        //-------------------------------------------------------------------------------------EDIT-----------
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {

            RoomLookupModel model = new RoomLookupModel();
            model.roomsEdit = db.Rooms.Find(id);
            model.facilityEdit = (from f in db.Facilities select f).ToList();
            ViewBag.noOfFacs = (from f in db.Facilities select f).Count();

            List<int> roomFac = (from i in db.RoomFacilities where i.RoomID == id select i.FacilityID).ToList();

            model.checkedFacs = roomFac;

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

            //  List<int> roomFac = (from i in db.RoomFacilities where i.RoomID == rooms.RoomID select i.FacilityID).ToList();

            //  model.checkedFacs = roomFac;

            IEnumerable<SelectListItem> selectList = from b in db.Buildings
                                                     select new SelectListItem
                                                     {
                                                         Value = b.BuildingCode,
                                                         Text = b.BuildingCode + " - " + b.Name

                                                     };

            ViewBag.EditDepartments = new SelectList(selectList, "Value", "Text");


            if (ModelState.IsValid)
            {
                if (rooms != null)
                {


                    db.Entry(rooms).State = EntityState.Modified;
                    db.SaveChanges();

                }



                List<RoomFacility> rFac = new List<RoomFacility>();
                rFac = (from r in db.RoomFacilities where r.RoomID == rooms.RoomID select r).ToList();

                foreach (RoomFacility r in rFac)
                {

                    db.Entry(r).State = EntityState.Deleted;
                    db.SaveChanges();


                }

                List<int> fac = (from f in db.Facilities select f.FacilityID).ToList();


                int facsNo = fac.Last();
                for (int i = 1; i <= facsNo; i++)
                {



                    if (Request["fac-" + i] == "1")
                    {

                        RoomFacility newRF = new RoomFacility();
                        newRF.RoomID = rooms.RoomID;
                        newRF.FacilityID = i;
                        db.Entry(newRF).State = EntityState.Added;
                        db.SaveChanges();


                    }


                }

                return RedirectToAction("Index");

            }

            return View(rooms);

        }
        //---------------------------------------------------------------------------------CREATE BUILDING-------------
        public ActionResult CreateBuilding()
        {

            IEnumerable<SelectListItem> parkCodesList = new List<SelectListItem>()
                {
                   
                    new SelectListItem() {Text="W - West", Value="W"},
                    new SelectListItem() {Text="C - Central", Value="C"},
                    new SelectListItem() {Text="E - East", Value="E"},
                   
             
               
                };

            ViewBag.parkCodes = parkCodesList;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBuilding(RoomLookupModel model)
        {

            Building buildings = new Building();
            buildings = model.buildingCreate;
            List<string> buildingCodes = (from b in db.Buildings select b.BuildingCode).ToList();

            if (buildingCodes.Contains((buildings.BuildingCode).ToUpper()))
            {

                TempData["notice"] = "This building code already exists. Please choose another.";
                return RedirectToAction("CreateBuilding", "RoomLookup");

            }

            if (ModelState.IsValid)
            {



                db.Entry(buildings).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            //Room room = new Room();
            //room = model.roomsCreated;

            //if (ModelState.IsValid)
            //{
            //    db.Entry(model.roomsCreate).State = EntityState.Added;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}






            return View("Index");
        }

        public ActionResult AddBuilding(Building buildings)
        {

            Room rooms = new Room();
            if (ModelState.IsValid)
            {


                //db.Entry(buildings).State = EntityState.Added;
                //db.SaveChanges();
                //return View("Index");
            }

            return View("Edit");
        }
        //-------------------------------------------------------------------------------------DELETE BUILDING---
        public ActionResult DeleteBuilding()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------VIEW BUILDINGS----
        public ActionResult ViewBuildings()
        {

            RoomLookupModel model = new RoomLookupModel();
            model.buildingsIndex = (from r in db.Buildings select r);

            return View(model);
        }

        //-------------------------------------------------------------------------------------VIEW ROOM---------
        public ActionResult ViewRoom(int id)
        {
            Room rooms = db.Rooms.Find(id);
            RoomLookupModel model = new RoomLookupModel();
            model.roomsDelete = rooms;


            model.roomfacView = (from f in db.RoomFacilities
                                 join r in db.Facilities on f.FacilityID equals r.FacilityID
                                 where f.RoomID == id
                                 select r).ToList();


            return View(model);
        }

        //-------------------------------------------------------------------------------------DELETE------------
        public ActionResult Delete(int id)
        {
            Room rooms = db.Rooms.Find(id);
            RoomLookupModel model = new RoomLookupModel();
            model.roomsDelete = rooms;

            model.roomfacView = (from f in db.RoomFacilities
                                 join r in db.Facilities on f.FacilityID equals r.FacilityID
                                 where f.RoomID == id
                                 select r).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(RoomLookupModel model)
        {
            Room room = new Room();
            room = model.roomsDelete;

            if (ModelState.IsValid)
            {

                db.Entry(model.roomsDelete).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Delete");
        }

        //---------------------------------------------------------------------------------CREATE---------------

        //
        // GET: /Movies/Create

        public ActionResult Create()
        {

            RoomLookupModel model = new RoomLookupModel();
            Room rooms = new Room();
            model.roomsCreate = rooms;


            IEnumerable<SelectListItem> selectList = from b in db.Buildings
                                                     select new SelectListItem
                                                     {
                                                         Value = b.BuildingCode,
                                                         Text = b.BuildingCode + " - " + b.Name

                                                     };
            ViewBag.EditDepartments = new SelectList(selectList, "Value", "Text");

            //copied from edit page for checkboxes

            model.facilityEdit = (from f in db.Facilities select f).ToList();


            List<int> roomFac = (from i in db.RoomFacilities where i.RoomID == 0 select i.FacilityID).ToList();
            //copied from edit page for checkboxes


            return View(model);
        }

        //
        // POST: /Movies/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomLookupModel model)
        {


            IEnumerable<SelectListItem> selectList = from b in db.Buildings
                                                     select new SelectListItem
                                                     {
                                                         Value = b.BuildingCode,
                                                         Text = b.BuildingCode + " - " + b.Name

                                                     };
            ViewBag.EditDepartments = new SelectList(selectList, "Value", "Text");

            model.facilityEdit = (from f in db.Facilities select f).ToList();


            List<int> roomFac = (from i in db.RoomFacilities where i.RoomID == 0 select i.FacilityID).ToList();


            Room rooms = new Room();
            rooms = model.roomsCreate;

            if (ModelState.IsValid)
            {
                db.Entry(rooms).State = EntityState.Added;
                db.SaveChanges();



                List<int> fac = (from f in db.Facilities select f.FacilityID).ToList();


                int facsNo = fac.Last();
                for (int i = 1; i <= facsNo; i++)
                {



                    if (Request["fac-" + i] == "1")
                    {

                        RoomFacility newRF = new RoomFacility();
                        newRF.RoomID = rooms.RoomID;
                        newRF.FacilityID = i;
                        db.Entry(newRF).State = EntityState.Added;
                        db.SaveChanges();


                    }


                }

                return RedirectToAction("Index");

            }


            TempData["notice"] = "Password is incorrect. Please try again.";




            return View(model);
        }

        //--------------------------------------------------------------















        public Room rooms { get; set; }

        public string sortOrder { get; set; }



    }
}

