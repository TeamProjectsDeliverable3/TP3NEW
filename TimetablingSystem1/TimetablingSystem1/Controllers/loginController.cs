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
        public loginController()
        {

        }
        DataAccessLayer dal = new DataAccessLayer();
        private TimetablingEntities db = new TimetablingEntities();

        [HttpGet]
        public ActionResult Index()
        {
            loginModel lmodel = new loginModel();

            //ViewBag.Password = new SelectList(db.Departments, "DepartmentCode", "PasswordHash");





            //var displaydeps = db.Departments.Where(s => s.DepartmentCode != null).ToList();

            IEnumerable<SelectListItem> selectList = from s in db.Departments
                                                     select new SelectListItem
                                                     {
                                                         Value = s.DepartmentCode,
                                                         Text = s.DepartmentCode + " - " + s.Name
                                                     };
            ViewBag.DisplayDepartments = new SelectList(selectList, "Value", "Text").Distinct();







            return View();

        }

        [HttpPost]
        public ActionResult Index(loginModel model)
        {


            //ViewBag.Password = new SelectList(db.Departments, "DepartmentCode", "PasswordHash");


            IEnumerable<SelectListItem> selectList = from s in db.Departments
                                                     select new SelectListItem
                                                     {
                                                         Text = s.DepartmentCode + " - " + s.Name,
                                                         Value = s.DepartmentCode

                                                     };
            ViewBag.DisplayDepartments = new SelectList(selectList, "Text", "Value");




            if (ModelState.IsValid)
            {
                //string passwordHash = dal.sha256(model.Password);
               // var departmentPassword = (from d in db.Departments where d.DepartmentCode == model.DepartmentCode select d.PasswordHash).ToString();
                 if (DataAccessLayer.UserIsValid(model.DisplayDepartments, model.Password))
              //  if (departmentPassword == passwordHash)
                {
                   
                    FormsAuthentication.SetAuthCookie(model.DisplayDepartments, false); //set to false: cookie is destroyed when browser is closed - user will have to login in again if browser is closed
                    return RedirectToAction("index", "AddRequest"); //page is redirected to the page 'index' which has the controller 'home'

                }
                else
                 {
                     

                         TempData["notice"] = "Password is incorrect. Please try again.";
                    

                    
                    return RedirectToAction("Index", "login");
                }


                
            }
            ModelState.AddModelError("", "Invalid password");
            ViewBag.incorrectPassword = "Invalid password";
            return RedirectToAction("Index", "login");
        }
    }
}