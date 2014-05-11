using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TimetablingSystem1.Models
{
    public class loginModel
    {
    
       // [Required]
        public string DepartmentCode { get; set; }

        [Required]
        public string Password { get; set; }

        public string DepartmentName { get; set; }

        [Required]
        public string DisplayDepartments { get; set; }

       

       

        
    }
}
