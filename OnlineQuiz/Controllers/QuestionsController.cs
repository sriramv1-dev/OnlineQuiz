using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace OnlineQuiz.Controllers
{
    public class QuestionsController : Controller
    {
        public ActionResult Index()
        {
            return View(new tbl_Questions
            {
                IsMultipleChoice = true
            });
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Questions q)
        {
            if (ModelState.IsValid)
            {
                using (OnlineQuizEntities oq = new OnlineQuizEntities())
                {
                    oq.tbl_Questions.Add(q);
                    oq.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = "Question Added Successfully";
                    return View("Create");
                }
            }
            else
            {
                ViewBag.Message = "Please provide all the values";
                return View(q);
            }
        }
    }
}
