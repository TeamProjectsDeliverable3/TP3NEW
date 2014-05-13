using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TimetablingSystem1.Models;


namespace TimetablingSystem1.Controllers.api
{
    public class RoomAPIController : ApiController
    {
        public TimetablingEntities db = new TimetablingEntities();
        public IEnumerable<Room> getSemester()
        {

            return db.Rooms;

        }

      

       public IEnumerable<Room> getRoom(string FacilityIDString, string ParkCode, string BuildingCode, string SeatingType, string RoomType, string Capacity)
        {
           JavaScriptSerializer serializer = new JavaScriptSerializer();
           IQueryable<Room> parkRoom = from r in db.Rooms select r;
           IQueryable<Room> buildingRoom = from r in db.Rooms select r;
           IQueryable<Room> seatingRoom = from r in db.Rooms select r;
           IQueryable<Room> typeRoom = from r in db.Rooms select r;
           IQueryable<Room> capacityRoom = from r in db.Rooms select r;
           IQueryable<Room> facRoom = from r in db.Rooms select r;
          
           if (ParkCode != null)
           {
               parkRoom = from r in db.Rooms
                                           join b in db.Buildings on r.BuildingCode equals b.BuildingCode
                                           where
                                               b.ParkCode == ParkCode
                                           select r;
           }

           if (BuildingCode != null) {

               buildingRoom = from r in db.Rooms where r.BuildingCode == BuildingCode select r;
           
           
           }

           if (SeatingType != null)
           {

               seatingRoom = from r in db.Rooms where r.SeatingType == SeatingType select r;


           }

           if (RoomType != null)
           {

               typeRoom = from r in db.Rooms where r.RoomType == RoomType select r;


           }

           if (Capacity != null)
           {
               int capacityInt = Int32.Parse(Capacity);
               capacityRoom = from r in db.Rooms where r.Capacity >= capacityInt select r;


           }


           if(FacilityIDString != "[]"){

               var facs = serializer.Deserialize<int[]>(FacilityIDString);

               var facsCurrent = facs[0];

               IQueryable<RoomFacility> rooms = db.RoomFacilities.Where(f => f.FacilityID == facsCurrent);
               IQueryable<Room> allRooms = from r in db.Rooms select r;

               

               for (int i = 0; i < facs.Length; i++) {
                  
                   facsCurrent = facs[i];
                   rooms = rooms.Where(f => f.FacilityID.Equals(facsCurrent));
               
               }
               
               if (rooms != null)
               {
                    facRoom = (from f in rooms join r in allRooms on f.RoomID equals r.RoomID select r);

                   
                  
               }

               
           }

           IQueryable<Room> result1 = from b in parkRoom join a in buildingRoom on b.RoomID equals a.RoomID select b;
           IQueryable<Room> result2 = from c in seatingRoom join b in result1 on c.RoomID equals b.RoomID select c;
           IQueryable<Room> result3 = from d in typeRoom join c in result2 on d.RoomID equals c.RoomID select d;
           IQueryable<Room> result4 = from e in capacityRoom join d in result3 on e.RoomID equals d.RoomID select e;
           IQueryable<Room> results = from f in facRoom join e in result4 on f.RoomID equals e.RoomID select f;


           


           return results;
            

        }
    }
}
