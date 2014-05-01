using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TimetablingSystem1.DataAccess;
using TimetablingSystem1.Models;




namespace TimetablingSystem.Controllers
{
    public class loginController : Controller
    {
        //
        // GET: /login/
	public loginController(){
       
	}
	 DataAccessLayer dal = new DataAccessLayer();
     private TimetablingEntities db = new TimetablingEntities();

     public ActionResult Index()
        
        {
            LoginModel lmodel = new LoginModel();

            var query = from ModuleCode in db.Modules
                        where ModuleCode.DepartmentCode == "CO"
                        select ModuleCode;

            //ViewBag.DepartmentCode = new SelectList(query, "ModuleCode", "ModuleCode");

            //ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentCode");
            //ViewBag.DepartmentName = new SelectList(db.Departments, "DepartmentCode", "Name");        
            ViewBag.Password = new SelectList(db.Departments, "DepartmentCode", "PasswordHash");

        



         var displaydeps = db.Departments.Where(s => s.DepartmentCode != null).ToList();
         
         IEnumerable<SelectListItem> selectList = from s in displaydeps
                                                  select new SelectListItem
                                                  {
                                                      Value = s.DepartmentCode,
                                                      Text = s.DepartmentCode + " - " + s.Name
                                                  };
         ViewBag.DisplayDepartments = new SelectList(selectList, "Value", "Text").Distinct();
        



        
            
                     
            return View();
         
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)

        {
            /*var query = from s in db.Modules
                        where s.DepartmentCode == "BS"
                        select s; */



           // ViewBag.DepartmentCode = new SelectList(query, "ModuleCode", "ModuleCode");



            var displaydeps = db.Departments.Where(s => s.DepartmentCode != null).ToList();

            IEnumerable<SelectListItem> selectList = from s in displaydeps
                                                     select new SelectListItem
                                                     {
                                                         Text = s.DepartmentCode + " - " + s.Name,
                                                         Value = s.DepartmentCode
                                                         
                                                     };
            ViewBag.DisplayDepartments = new SelectList(selectList, "Text" , "Value");
            

            if (ModelState.IsValid) 
            {
                
                    if (DataAccessLayer.UserIsValid(model.DisplayDepartments, model.Password))
                    {
                        string selectedDepartment = model.DisplayDepartments;
                        Session["department"] = selectedDepartment;
                        FormsAuthentication.SetAuthCookie(model.DisplayDepartments, false); //set to false: cookie is destroyed when browser is closed - user will have to login in again if browser is closed
                        return RedirectToAction("index", "AddRequest", selectedDepartment); //page is redirected to the page 'index' which has the controller 'home'
                        
                    }
                    {
                        ModelState.AddModelError("", "Invalid password");
                    }
                
              
            }  
            return View();
        }
	}
}