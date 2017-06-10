using OnlineQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OnlineQuiz.Controllers
{
    public class QuizController : Controller
    {
        // GET: Quiz
        public ActionResult Index(tbl_Questions q, tbl_Quiz qq, QuizQuestions qqq, string QuizId)
        {
            QuizQuestions qqs = new QuizQuestions();
            if (QuizId != null && QuizId != "")
            {

                //qqs = ViewBag.Quiz;


                using (OnlineQuizEntities oq = new OnlineQuizEntities())
                {
                    if (TempData["quizdrpdwn"] as QuizQuestions == null)
                    {
                        var quizdata = oq.tbl_Quiz.ToList();
                        foreach (var item in quizdata)
                        {
                            qqs.QuizList.Add(new SelectListItem { Text = item.QuizName, Value = item.QuizId.ToString() });
                        }
                        TempData["quizdrpdwn"] = qq;
                    }
                    else
                    {
                        qqs = TempData["quizdrpdwn"] as QuizQuestions;
                    }

                    var allqs = oq.tbl_Questions.ToList();
                    var allquizqu = oq.tbl_QuizQuestionRelation.ToList();
                    var quizqus = allquizqu.Where(a => a.QuizId == Convert.ToInt32(QuizId)).ToList();

                    List<tbl_Questions> actualqs = new List<tbl_Questions>();

                    allqs.Intersect<tbl_Questions>(allqs);


                    var resp = from a in allqs
                               join b in quizqus on a.QuestionId equals b.QuestionId
                               select a;

                    foreach (var item in resp)
                    {
                        actualqs.Add(item);
                    }
                    ViewBag.AllQuestions = actualqs;
                }
                return View("Quiz", qqs);
            }
            return View("Quiz", qqs);
        }
        public ActionResult Create()
        {
            using (OnlineQuizEntities oq = new OnlineQuizEntities())
            {
                var questionsdata = oq.tbl_Questions.ToList();
                ViewBag.Questions = questionsdata;
            }
            return View();
        }
        public ActionResult CreateQuiz(tbl_Quiz q, string hid_questions)
        {
            if (ModelState.IsValid)
            {
                using (OnlineQuizEntities oq = new OnlineQuizEntities())
                {
                    oq.tbl_Quiz.Add(q);
                    oq.SaveChanges();
                    if (hid_questions != "")
                    {
                        foreach (string questionid in hid_questions.Split(','))
                        {
                            tbl_QuizQuestionRelation qq = new tbl_QuizQuestionRelation();
                            qq.QuestionId = Convert.ToInt32(questionid);
                            qq.QuizId = q.QuizId;
                            oq.tbl_QuizQuestionRelation.Add(qq);
                            oq.SaveChanges();
                        }
                    }
                    ModelState.Clear();
                    var questionsdata = oq.tbl_Questions.ToList();
                    ViewBag.Questions = questionsdata;
                    ViewBag.Message = "Successfully created";
                }
                return View("Create");
            }
            using (OnlineQuizEntities oq = new OnlineQuizEntities())
            {
                ViewBag.Questions = oq.tbl_Questions.ToList();
            }
            ViewBag.Message = "Please provide all the values";
            return View("Create");
        }
        public ActionResult Quiz()
        {
            using (OnlineQuizEntities oq = new OnlineQuizEntities())
            {
                var quiz = oq.tbl_Quiz.ToList();
                var allquestions = oq.tbl_Questions.ToList();


                QuizQuestions qq = new QuizQuestions();
                foreach (var item in quiz)
                {
                    qq.QuizList.Add(new SelectListItem { Text = item.QuizName, Value = item.QuizId.ToString() });

                }
                ViewBag.AllQuestions = allquestions;
                ViewBag.Quiz = qq;
                TempData["quizdrpdwn"] = qq;
                return View(qq);
            }
        }
        public ActionResult LoadQuestions(string ddlVendor)
        {
            return View("Quiz");
        }

    }
}