using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineQuiz.Models
{
    public class QuizQuestions
    {
        public QuizQuestions()
        {
            QuizList = new List<SelectListItem>();
        }
        public List<SelectListItem> QuizList
        {
            get;
            set;
        }
    }
}