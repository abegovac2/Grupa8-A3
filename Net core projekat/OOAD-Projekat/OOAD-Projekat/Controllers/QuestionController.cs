using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IQuestionRecommendation questionRecommendation;
        public QuestionController(IQuestionsRepository questionsRepository, IQuestionRecommendation questionRecommendation)
        {
            this.questionsRepository = questionsRepository;
            this.questionRecommendation = questionRecommendation;
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
        public async Task<IActionResult> Find([FromQuery(Name = "searchParam")] string SearchParam)
        {
            return View("Index", await questionsRepository.Find(SearchParam));
        }
        [HttpGet("byTag")]
        public async Task<IActionResult> ByTag([FromQuery(Name = "tagName")] string tagName)
        {
            return View("Index", await questionsRepository.ByTag(tagName.Trim().ToUpper()));
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
        public async Task<IActionResult> Recommended()
        {
            return View("Index", await questionRecommendation.RecommendQuestions(User.Identity.Name.ToString()));
        }
        // GET: Questions/Create
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string content, string tags)
        {
            var question = new Question();
            if (ModelState.IsValid)
            {
                question.TimeStamp = DateTime.UtcNow;
                question.Content = content;
                question.Title = title;

                await questionsRepository.AddQuestion(question);

                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }
    }
}
