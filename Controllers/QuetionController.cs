using QuetionBankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuetionBankApp.Controllers
{
    public class QuetionController : Controller
    {
        // GET: Quetion
        static List<QuetionTable> SavedQuetion = new List<QuetionTable>();
        static List<QuetionTable> Searchedquetions = new List<QuetionTable>();
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult AllQuetions()
        {
            var context = new QuetionBankEntities();
            var records = context.QuetionTables.ToList();//SELECT * FROM EMPTABLE....
            return View(records);
        }
        public ActionResult AddNew()
        {
            var newRec = new QuetionTable();//Create a Blank object
            return View(newRec);//Inject it as model to the View...
        }
        [HttpPost]
        public ActionResult AddNew(QuetionTable postedData)
        {
            //Create the Context object..
            var context = new QuetionBankEntities();
            //Add the new record to the EmpTables Collecion
            context.QuetionTables.Add(postedData);
            //Save the changes
            context.SaveChanges();
            //Redirect to the AllRecords..
            return RedirectToAction("AllQuetions");
        }

        public ViewResult ViewRec(string id)
        {
            var context = new QuetionBankEntities();
            var QnId = int.Parse(id);
            var model = context.QuetionTables.Where((qn) => qn.QuetionId == QnId).FirstOrDefault();//SELECT * From EmpTable where Id = empId;
            return View(model);
        }
        public ActionResult SaveQuestion(string id)
        {

            var context = new QuetionBankEntities();
            var QnId = int.Parse(id);
            var model = context.QuetionTables.Where((qn) => qn.QuetionId == QnId).FirstOrDefault();
            //SELECT * From EmpTable where Id = empId;
            SavedQuetion.Add(model);
            return RedirectToAction("AllQuetions");

        }
        public ViewResult SavedQuetions()
        {
            var context = new QuetionBankEntities();
            var records = SavedQuetion.ToList();//SELECT * FROM EMPTABLE....
            return View(records);
        }

        public ActionResult SearchBYKeyword()
        {
            var newRec = new QuetionTable();//Create a Blank object
            return View(newRec);
        }
       [HttpPost]
             public ActionResult SearchBYKeyword(QuetionTable postedData)
          {
            var context = new QuetionBankEntities();
            var rec = context.QuetionTables.ToList();


          //  var model = context.QuetionTables.Where((qn) => qn.Keyword == postedData.SearchKeyword).FirstOrDefault();
          
            /*foreach(var re in rec)
            {
                if (re.Keyword == postedData.SearchKeyword)
                {
                    Searchedquetions.Add(re);
                }
            }
            */
            foreach(var re in rec)
            {
                var word = re.Quetion;
                if(word.Contains(postedData.SearchKeyword))
                {
                    Searchedquetions.Add(re);
                }
            }
           // Searchedquetions.Add(model);
            return View();
        }


        public ActionResult Search(QuetionTable postedData)
        {
            var context = new QuetionBankEntities();
            var records = Searchedquetions.ToList();//SELECT * FROM EMPTABLE....
            return View(records);

        }
        public ActionResult UpdateRec(QuetionTable postedData)
        {
            //create the context object.
            var context = new QuetionBankEntities();
            //Find the matching record
            var model = context.QuetionTables.FirstOrDefault((e) => e.QuetionId == postedData.QuetionId);
            if (model == null) throw new Exception("This is not found to update");
            //Set the new values to the old record
            model.Quetion = postedData.Quetion;
            model.SubjectType = postedData.SubjectType;
            model.Option1 = postedData.Option1;
            model.Option2 = postedData.Option2;
            model.Option3 = postedData.Option3;
            model.Option4 = postedData.Option4;
            model.Keyword= postedData.Keyword;
            //Save the changes
            context.SaveChanges();
            //Return to the view.
            return RedirectToAction("AllQuetions");//Redirecting to the method called AllRecord
        }

        public ActionResult Delete(string id)
        {
            var context = new QuetionBankEntities();
            var QnId = int.Parse(id);
            var model = context.QuetionTables.Where((qn) => qn.QuetionId == QnId).FirstOrDefault();//SELECT * From EmpTable where Id = empId;
            context.QuetionTables.Remove(model);
            context.SaveChanges();
            return RedirectToAction("AllQuetions");
        }
    }
}