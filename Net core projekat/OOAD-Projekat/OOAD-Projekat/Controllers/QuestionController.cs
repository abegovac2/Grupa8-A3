using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyQuestions()
        {
            // Console.WriteLine(User.Identity.Name);
            return View("Index");
        }
    }
}
