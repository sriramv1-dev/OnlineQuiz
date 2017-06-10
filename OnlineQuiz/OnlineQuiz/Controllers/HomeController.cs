using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using OnlineQuiz.Models;

namespace OnlineQuiz.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tbl_Users u)
        {
            if (ModelState.IsValid)
            {
                using (OnlineQuizEntities dc = new OnlineQuizEntities())
                {
                    var result = dc.tbl_Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (result != null)
                    {
                        Session["LoggedUserID"] = result.UserId.ToString();
                        Session["LoggedUserName"] = result.UserName.ToString();
                        return RedirectToAction("AfterLogin");
                    }
                }
            }
            return View(u);
        }
        public ActionResult afterLogin()
        {
            if (Session["LoggedUserID"] != null)
            {
                using (OnlineQuizEntities dc = new OnlineQuizEntities())
                {
                    var quizdata = dc.tbl_Quiz.ToList();
                    var questionsdata = dc.tbl_Questions.ToList();
                    ViewBag.Questions = questionsdata;
                    ViewBag.QuizData = quizdata;
                    return View();
                }
            }
            else
                return RedirectToAction("Index");
        }
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(int quizid)
        {
            if (Session["LoggedUserID"] != null)
            {
                using (OnlineQuizEntities dc = new OnlineQuizEntities())
                {
                    var quizdata = dc.tbl_Quiz.ToList();
                    var quiz = quizdata.Where(a => a.QuizId == quizid).ToList();


                    var quizrelation = dc.tbl_QuizQuestionRelation.ToList();
                    var qzrlz = quizrelation.Where(a => a.QuizId == quizid).ToList();


                    IList<tbl_Questions> questionsdata = dc.tbl_Questions.ToList();
                    //IList<tbl_Questions> qs = new List<tbl_Questions>();
                    //foreach (var item in qzrlz)
                    //{
                    //    qs.Add(questionsdata.Where(a => a.QuestionId == item.QuestionId).FirstOrDefault());

                    //}
                    foreach (var item in questionsdata)
                    {
                        int count = qzrlz.Where(a => a.QuestionId == item.QuestionId).Count();
                        if (count > 0)
                        {
                            item.IsQuizQuestion = true;
                        }
                        else
                            item.IsQuizQuestion = false;

                    }
                    ViewBag.AllQuestions = questionsdata;


                    //var isq = qs.Where(a => a.QuestionId == 1).ToList().Count();
                    //ViewBag.Questions = qs;
                    ViewBag.QuizData = quiz;
                    return View();
                }
            }
            else
                return RedirectToAction("Index");
        }
        public ActionResult UpdateQuiz(int quizid, tbl_Questions qs, string hid_qids)
        {
            using (OnlineQuizEntities oq = new OnlineQuizEntities())
            {
                if (hid_qids == null)
                    hid_qids = "1,3";
                foreach (var qids in hid_qids.Split(','))
                {
                    tbl_QuizQuestionRelation qq = new tbl_QuizQuestionRelation();
                    qq.QuestionId = Convert.ToInt32(qids);
                    qq.QuizId = quizid;
                    oq.tbl_QuizQuestionRelation.Add(qq);
                    oq.SaveChanges();
                }
                ModelState.Clear();
            }
            return RedirectToAction("AfterLogin");
        }

    }
}