using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TimetablingSystem1.Models
{
    public class ViewRequestModel
    {
        public ViewRequestModel() { 
        
        
        }
        public int requestID { get; set; }
        public string moduleCode { get; set; }
        public string moduleTitle { get; set; }
        public string days { get; set; }
        public int daysint { get; set; }
        public string startTime { get; set; }
        public int startint { get; set; }
        public string endTime { get; set; }
        public int endint { get; set; }
        public string weeks { get; set; } 
        public byte numberOfRooms { get; set; }
        public byte priority { get; set; }

        public string round { get; set; }
        public String prefRooms { get; set; }
        public String prefFacs { get; set; }
        public string specialRequirements { get; set; }
        public string period { get; set; }
        public string length { get; set; }

        public string status { get; set; }
        public string statusCode { get; set; }
        
        public Request requestIndex { get; set;}
        public Module moduleIndex { get; set; }

        public Request requestDelete { get; set; }

        public string requestData { get; set; }

    }
}