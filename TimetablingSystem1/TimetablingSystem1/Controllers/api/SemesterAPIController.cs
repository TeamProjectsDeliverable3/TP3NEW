using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using TimetablingSystem1.Models;

namespace TimetablingSystem1.Controllers.api
{
    public class SemesterAPIController : ApiController
    {
        public TimetablingEntities db = new TimetablingEntities();
        public IEnumerable<Semester> getSemester()
        {

            return db.Semesters;

        }

        public IEnumerable<Round> getSemester(string SemesterID)
        {
            int semIDint = Int32.Parse(SemesterID);
            var semesterData = from r in db.Rounds where r.SemesterID == semIDint select r;

            if (semesterData == null)
            {

                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            }

            return semesterData;

        }
    }
}
