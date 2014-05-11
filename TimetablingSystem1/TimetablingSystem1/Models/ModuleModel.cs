using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TimetablingSystem1.Models
{
    public class ModuleModel 
    {
        public IEnumerable<Module> modulesIndex { get; set; }


        public Module modulesEdit { get; set; }

        public Department department { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Department Code is required")]
        [StringLength(2, ErrorMessage = "Department Code should be 2 characters.", MinimumLength = 2)]
        public string DepartmentCode { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Module Code is required")]
        [StringLength(6, ErrorMessage = "Module Code should be 6 characters.", MinimumLength = 6)]
        public string ModuleCode { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title is too long, please make it shorter.", MinimumLength = 10)]
        public string Title { get; set; }



    }
}