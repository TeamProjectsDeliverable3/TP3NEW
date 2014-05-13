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

        [Required(ErrorMessage = "Password is incorrect")]
        public string Password { get; set; }

        public string DepartmentName { get; set; }

        
        [Required(ErrorMessage = "Department Code is required")]
        [StringLength(2, ErrorMessage = "Department Code should be 2 characters.", MinimumLength = 2)]
        public string DisplayDepartments { get; set; }

       

       

        
    }
}
