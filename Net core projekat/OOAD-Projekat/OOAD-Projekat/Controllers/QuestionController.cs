using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Data;
using OOAD_Projekat.Data.Questions;
using OOAD_Projekat.Models;
using OOAD_Projekat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionsRepository questionsRepository;
        public QuestionController(IQuestionsRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await questionsRepository.FindAll());
        }
        [Authorize]
        public async Task<IActionResult> MyQuestions()
        {
            return View("Index", await questionsRepository.FindMine(User.Identity.Name));
        }

        [HttpGet]
        public async Task<IActionResult> Pretrazi([FromQuery(Name = "searchParam")] string SearchParam)
        {
            return View("Index", await questionsRepository.Find(SearchParam));
        }
        // TODO
        public IActionResult Popular()
        {
            return View("Index", new List<Question>());
        }
        // TODO
        public IActionResult Unanswered()
        {
            return View("Index", new List<Question>());
        }
        // TODO
        public IActionResult Recommended()
        {
            return View("Index", new List<Question>());
        }
    }
}
