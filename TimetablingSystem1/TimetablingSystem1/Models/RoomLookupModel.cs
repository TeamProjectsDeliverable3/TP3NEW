using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TimetablingSystem1.Models
{
    public class RoomLookupModel
    {
        public IEnumerable<Room> roomsIndex { get; set; }

        public IEnumerable<Building> buildingsIndex { get; set; }

        public Room roomsEdit { get; set; }

        public Room roomsDelete { get; set; }

        public Room roomsCreate { get; set; }

        public Building buildingCreate { get; set; }

        public List<int> checkedFacs { get; set; }



        public IEnumerable<Facility> facilityEdit { get; set; }

        public IEnumerable<RoomFacility> roomFac { get; set; }

        public int roomfacs { get; set; }

        public bool facilityIsChecked { get; set; }

        public Building building { get; set; }

        public string EditDepartments { get; set; }

        public string Name { get; set; }

        public string AllFacilities { get; set; }

        public string BuildingCode { get; set; }
        public string ParkCode { get; set; }
        public string RoomType { get; set; }
        public string SeatingType { get; set; }
        public string Capacity { get; set; }








        public IEnumerable<string> Facs { get; set; }

        //public IEnumerable<Room> order { get; set; }

        public RoomLookupModel()
        {
            TimetablingEntities db = new TimetablingEntities();



        }












        public Room roomsCreated { get; set; }
    }
}