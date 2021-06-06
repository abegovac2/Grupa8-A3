using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OOAD_Projekat.Data;
using OOAD_Projekat.Data.Questions;
using OOAD_Projekat.Data.Tags;
using OOAD_Projekat.Models;
using OOAD_Projekat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionsRepository questionsRepository;
        private readonly IQuestionRecommendation questionRecommendation;
        private readonly ITagsRepository tagsRepository;
        public QuestionController(IQuestionsRepository questionsRepository, IQuestionRecommendation questionRecommendation, ITagsRepository tagsRepository)
        {
            this.questionsRepository = questionsRepository;
            this.questionRecommendation = questionRecommendation;
            this.tagsRepository = tagsRepository;
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
        [Authorize]
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
        public async Task<IActionResult> Create( string title, string content, string tags)
        {
            var question = new Question();
            if (ModelState.IsValid)
            {
                question.TimeStamp = DateTime.UtcNow;
                question.Content = content;
                question.Title = title;
                int id = question.Id;
                await questionsRepository.AddQuestion(question);

                if (tags != null)
                {
                    tags = tags + ",";
                    string[] listOfTags = (tags).Split(",");
                    for (int i = 0; i < listOfTags.Length -1; i++)
                    {
                        Tag t = new Tag();
                        t.TagContent = listOfTags[i];
                        t.NumOfUses = 1;
                        await tagsRepository.AddTags(t);
                    }
                }
            
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }
        // Metoda koja se poziva kada se udje u detalj pitanja, kako bi sistem za preporuke ispravno radio
        public async Task SaveOpening(Question question)
        {
            await questionsRepository.SaveOpening(User.Identity.Name.ToString(), question);
        }
    }
}
