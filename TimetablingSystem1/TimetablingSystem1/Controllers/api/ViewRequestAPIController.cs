using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TimetablingSystem1.Models;

namespace TimetablingSystem1.Controllers.api
{
    public class ViewRequestAPIController : ApiController
    {
        TimetablingEntities db = new TimetablingEntities();

        public ViewRequestModel getRequestData() {

            ViewRequestModel model = new ViewRequestModel();

            return model;
        }
        public ViewRequestModel getRequestData(string RequestID)
        {
            int reqid = Convert.ToInt32(RequestID);
            Request viewRequest = new Request();
            viewRequest = db.Requests.Find(reqid);
            List<Room> prefRoom = (from f in db.PreferenceRooms
                                              join r in db.Rooms on f.RoomID equals r.RoomID
                                              where f.RequestID == reqid
                                              select r).ToList();

            List<Facility> prefFac = (from f in db.RequiredFacilities
                                      join fa in db.Facilities on f.FacilityID equals fa.FacilityID
                                      where f.FacilityID == reqid
                                      select fa).ToList();

            ViewRequestModel model = new ViewRequestModel();

            foreach (var item in prefRoom) {

                model.prefRooms += item.Name + ", ";
            
            }

            foreach (var item in prefFac)
            {

                model.prefFacs = item.Name + ", ";

            }

            model.specialRequirements = viewRequest.SpecialRequirements;
            model.priority = viewRequest.Priority;
            model.period = (viewRequest.Period).ToString();
            model.length = (viewRequest.Length).ToString();
            model.status = viewRequest.Status;


            
            




            return model;
        }


        
      
    } 
}
