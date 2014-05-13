using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TimetablingSystem1.Models
{
    public class AddRequestModel
    {
        public string periodSelect { get; set; }
        public string lengthData { get; set; }
        public Request requestsIndex { get; set; }
        public RequestWeek requestweek { get; set; }



        public IEnumerable<Facility> facilities { get; set; }

        public Facility facilitiesIndex { get; set; }

        public Round rounds { get; set; }

        public PreferenceRoom preferenceRooms { get; set; }

        public string departmentModules { get; set; }

        public Module modules { get; set; }
    }
}