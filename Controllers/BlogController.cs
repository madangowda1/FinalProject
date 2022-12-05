using MINIMVCPROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MINIMVCPROJECT.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        // GET: Blog
        [AllowAnonymous]
        public ActionResult AllBlogs()
        {

            var context = new MiniProjectBlogsEntities2();
            var data = context.BlogsTables.ToList();
            return View(data);
        }

        public ActionResult AddNew()
        {
            
            if (Session["currentUser"] == null)
            {
                Console.WriteLine("Please login Before U Post a Blog!!");
                TempData["ErrorInfo"] = "Please login Before U Post a Blog!!!";
                TempData.Keep();
                return RedirectToAction("SignIn", "Register");
            }
            var user = Session["currentUser"] as UserTable;
            var blog = new BlogsTable();
            blog.UId = user.UserId;
            blog.UserTable = user;
            return View(blog);
        }
      
        [HttpPost]
        public ActionResult AddNew(BlogsTable blog)
        {
            var context = new MiniProjectBlogsEntities2();
            context.BlogsTables.Add(blog);
            context.SaveChanges();
            return RedirectToAction("SignIn", "Register");
        }

        public ActionResult Delete(string id)
        {
            var context = new MiniProjectBlogsEntities2();
            var BId = int.Parse(id);
            var model = context.BlogsTables.Where((blog) => blog.BlogId == BId).FirstOrDefault();//SELECT * From EmpTable where Id = empId;
            context.BlogsTables.Remove(model);
            context.SaveChanges();
            return RedirectToAction("SignIn", "Register");
        }

        public ActionResult DisplayParticularBlogs()
        {

            if (Session["currentUser"] == null)
            {
                Console.WriteLine("Please login Before you Edit a Blog!!");
                TempData["ErrorInfo"] = "Please login Before U Edit a Blog!!!";
                TempData.Keep();
                return RedirectToAction("SignIn", "Register");
            }

            var context = new MiniProjectBlogsEntities2();
            var data = context.BlogsTables.ToList();
            return View(data);
        }
        public ViewResult ViewRec(string id)
        {
            var context = new MiniProjectBlogsEntities2();
            var BId = int.Parse(id);
            var model = context.BlogsTables.Where((blog) => blog.BlogId == BId).FirstOrDefault();//SELECT * From EmpTable where Id = empId;
            return View(model);
        }

        public ActionResult UpdateRec(BlogsTable postedData)
        {
            //create the context object.
            var context = new MiniProjectBlogsEntities2();
            //Find the matching record
            var model = context.BlogsTables.FirstOrDefault((e) => e.BlogId == postedData.BlogId);
            if (model == null) throw new Exception("This is not found to update");
            //Set the new values to the old record
            model.Messages = postedData.Messages;
            model.TravelDate = postedData.TravelDate;
            model.Images = postedData.Images;
            model.Details = postedData.Details;
            //Save the changes
            context.SaveChanges();
            //Return to the view.
            return RedirectToAction("AllBlogs");//Redirecting to the method called AllRecord
        }

    }
}