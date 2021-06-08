using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OOAD_Projekat.Data;
using OOAD_Projekat.Data.NotificationData;
using OOAD_Projekat.Data.Questions;
using OOAD_Projekat.Data.TagPosts;
using OOAD_Projekat.Data.Tags;
using OOAD_Projekat.Models;
using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
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
        private readonly ITagPostRepository tagPostRepository;
        private readonly INotificationRepository notificationRepository;
        public QuestionController(IQuestionsRepository questionsRepository, IQuestionRecommendation questionRecommendation, ITagsRepository tagsRepository, ITagPostRepository tagPostRepository, IUserConnectionManager userConnectionManager, ApplicationDbContext context)
        {
            this.questionsRepository = questionsRepository;
            this.questionRecommendation = questionRecommendation;
            this.tagsRepository = tagsRepository;
            this.tagPostRepository = tagPostRepository;
            this.notificationRepository = new NotificationRepository(context, userConnectionManager);
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        // GET: Questions/CreateNewQuestion
        [Authorize]
        public async Task<IActionResult> CreateNewQuestion(string title, string content)
        {
            var user = await questionsRepository.getUserByUserName(User.Identity.Name);

            if (user == null) return NotFound();

            var question = new Question { 
                Title = title,
                Content = content,
                Duplicate = false,
                HotQuestion = false,
                User = user,
                TimeStamp = DateTime.Now,
                //Tags = nesto treba dodat
            };
            await questionsRepository.AddQuestion(question);

            await notificationRepository.AddUserToNotificationList(user.Id,question.Id,NotificationType.QUESTION);

            return RedirectToAction("Details",new { question.Id });
        }        
        //todo edit question
         public IActionResult Edit(int questionId)
        {
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null) return NotFound();

            var question = await questionsRepository.getQuestion((int)id);

            if (question == null) return NotFound();

            if(User.Identity.Name != null)  await questionsRepository.SaveOpening(User.Identity.Name, question);

            return View(question);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,Tags")] QuestionViewModel q)
        {
            var question = new Question();
            if (ModelState.IsValid)
            {
                question.TimeStamp = DateTime.UtcNow;
                question.Content = q.Content;
                question.Title = q.Title;
                await questionsRepository.AddQuestion(question);
                var AddedQuestion = await questionsRepository.getLastQuestion();
                if (q.Tags != null)
                {
                    q.Tags = q.Tags + ",";
                    string[] listOfTags = (q.Tags).Split(",");
                    for (int i = 0; i < listOfTags.Length -1; i++)
                    {
                        Tag t = new Tag();
                        t.TagContent = listOfTags[i];
                        t.NumOfUses = 1;
                        await tagsRepository.AddTags(t);
                        var addedTag = await tagsRepository.GetTagByName(listOfTags[i]);
                        TagPost tp = new TagPost();
                        tp.QuestionId = AddedQuestion.Id;
                        tp.TagId = addedTag.Id;
                        await tagPostRepository.AddTagPost(tp);
                    }
                }
            
                return RedirectToAction(nameof(Index));
            }
            return View(q);
        }
        // Metoda koja se poziva kada se udje u detalj pitanja, kako bi sistem za preporuke ispravno radio
        public async Task SaveOpening(Question question)
        {
            await questionsRepository.SaveOpening(User.Identity.Name.ToString(), question);
        }
    }
}
