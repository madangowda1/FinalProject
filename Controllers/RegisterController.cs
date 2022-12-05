using MINIMVCPROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MINIMVCPROJECT.Controllers
{

    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

     

        public ActionResult AllUser() 
        {
            var context = new MiniProjectBlogsEntities2();
            var records = context.UserTables.ToList();
            return View(records);
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string username, string password)
        {
            var data = HttpContext.Application["userList"] as Dictionary<string, string>;
            var context = new MiniProjectBlogsEntities2();
            var user = context.UserTables.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                Session["currentUser"] = user; //To store it till the User logs off!!!
                FormsAuthentication.RedirectFromLoginPage(username, true);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorInfo = "Login has failed!";
                return View();
            }
        }
        public ActionResult Register() 
        {
            var newRec = new UserTable();
            return View(newRec);
        }

        [HttpPost]
        public ActionResult Register(UserTable postedData)
        {
            //Create the Context object..
            var context = new MiniProjectBlogsEntities2();
            //Add the new record to the EmpTables Collecion
            context.UserTables.Add(postedData);
            //Save the changes
            context.SaveChanges();
            //Redirect to the AllRecords..
            return RedirectToAction("AllUser");
        }

        public ActionResult Delete(string id)
        {
            var context = new MiniProjectBlogsEntities2();
            var UserId1 = int.Parse(id);
            var model = context.UserTables.Where((emp) => emp.UserId == UserId1).FirstOrDefault();//SELECT * From EmpTable where Id = empId;
            context.UserTables.Remove(model);
            context.SaveChanges();
            return RedirectToAction("AllUser");
        }
        
 public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }




    }
}