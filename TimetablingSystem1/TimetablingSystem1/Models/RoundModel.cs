using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimetablingSystem1.Models
{
    public class RoundModel
    {
        public Round rounds { get; set; }
        public Semester semesters { get; set; }

        public SelectList currentRounds { get; set; }

        public SelectList nonActiveSemesters { get; set; }

        public string academicDates { get; set; }


    }
}