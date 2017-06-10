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
        public ActionResult Index()
        {
            return View();
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
            using (OnlineQuizEntities oq = new OnlineQuizEntities())
            {
                oq.tbl_Quiz.Add(q);
                oq.SaveChanges();

                foreach (string questionid in hid_questions.Split(','))
                {
                    tbl_QuizQuestionRelation qq = new tbl_QuizQuestionRelation();
                    qq.QuestionId = Convert.ToInt32(questionid);
                    qq.QuizId = q.QuizId;
                    oq.tbl_QuizQuestionRelation.Add(qq);
                    oq.SaveChanges();
                }

                ModelState.Clear();
                var questionsdata = oq.tbl_Questions.ToList();
                ViewBag.Questions = questionsdata;
            }
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
                    qq.QuizList.Add(new SelectListItem { Text=item.QuizName,Value=item.QuizId.ToString()});

                }
                ViewBag.AllQuestions = allquestions;
                ViewBag.Quiz = qq;
                return View(qq);
            }
        }

    }
}