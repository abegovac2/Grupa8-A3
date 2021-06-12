using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OOAD_Projekat.Data;
using OOAD_Projekat.Data.NotificationData;
using OOAD_Projekat.Data.Questions;
using OOAD_Projekat.Data.ReactionData;
using OOAD_Projekat.Data.TagPosts;
using OOAD_Projekat.Data.Tags;
using OOAD_Projekat.Data.Users;
using OOAD_Projekat.Models;
using OOAD_Projekat.Models.QuestionAndAnwserModels.RatingModels;
using OOAD_Projekat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        private readonly IReactionRepository reactionRepository;
        private readonly IUsersRepository usersRepository;

        public QuestionController(IQuestionsRepository questionsRepository,
            IQuestionRecommendation questionRecommendation,
            ITagsRepository tagsRepository,
            ITagPostRepository tagPostRepository,
            IUserConnectionManager userConnectionManager,
            IReactionRepository reactionRepository,
            IUsersRepository usersRepository,
            ApplicationDbContext context)
        {
            this.questionsRepository = questionsRepository;
            this.questionRecommendation = questionRecommendation;
            this.tagsRepository = tagsRepository;
            this.tagPostRepository = tagPostRepository;
            this.notificationRepository = new NotificationRepository(context, userConnectionManager);
            this.reactionRepository = reactionRepository;
            this.usersRepository = usersRepository;
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
        public async Task<IActionResult> Popular()
        {
            return View("Index", await questionsRepository.getPopularQestions());
        }
        // TODO
        public async Task<IActionResult> Unanswered()
        {
            return View("Index", await questionsRepository.FindUnanwseredQuestions());
        }
        // TODO
        [Authorize]
        public async Task<IActionResult> Recommended()
        {
            return View("Index", await questionRecommendation.RecommendQuestions(User.Identity.Name.ToString()));
        }
        [Authorize]
        public async Task<IActionResult> CreateAsync()
        {
            var user = await questionsRepository.getUserByUserName(User.Identity.Name);
            if (user == null) return NotFound();

            QuestionViewModel qvm = new QuestionViewModel();
            qvm.PopularTags = await tagsRepository.GetPopular();

            return View(qvm);
        }
        //todo need a view
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var question = await questionsRepository.getQuestion((int)id);

            if (question == null) return NotFound();

            var user = await questionsRepository.getUserByUserName(User.Identity.Name);
            if (user == null) return NotFound();

            StringBuilder builder = new StringBuilder();
            foreach (var tag in question.Tags)
            {
                builder.Append(tag.Tag.TagContent).Append(",");
            }
            var popularTags = await tagsRepository.GetPopular();
            var qvm = new QuestionViewModel((int)id, question.Title, question.Content, builder.ToString(), popularTags, user);

            return View(qvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Title,Content,Tags,User,Id,PopularTags")] QuestionViewModel q)
        {
            var question = await questionsRepository.getQuestion(q.Id);
            if (ModelState.IsValid)
            {
                question.Content = q.Content;
                question.Title = q.Title;
                var oldTags = question.Tags; 
                await questionsRepository.UpdateQuestion(question);
                
                if (q.Tags != null)
                {
                    q.Tags = q.Tags + ",";
                    string[] listOfTags = (q.Tags).Split(",");

                    var listOfOldTags = oldTags.Select(x => x.Tag.TagContent).ToList<String>();
                    var newTags = listOfTags.Except(listOfOldTags).ToArray<String>();
                    var changedTags = listOfOldTags.Except(listOfTags).ToArray<String>();

                    for (int i = 0; i < newTags.Length - 1; i++)
                    {
                        Tag t = new Tag();
                        t.TagContent = newTags[i];
                        t.NumOfUses = 1;
                        await tagsRepository.AddTags(t);
                        var addedTag = await tagsRepository.GetTagByName(newTags[i]);
                        TagPost tp = new TagPost();
                        tp.QuestionId = question.Id;
                        tp.TagId =  addedTag.Id;

                        await tagPostRepository.AddTagPost(tp);
                    }
                    for(int i = 0; i < changedTags.Length ; i++)
                    {
                        Tag t = await tagsRepository.GetTagByName(changedTags[i]);
                        await tagsRepository.DeleteTags(t);
                    }
                }
                

                return RedirectToAction("Details", new { question.Id });
            }
            q.PopularTags = await tagsRepository.GetPopular();
            return View(q);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null) return NotFound();

            var question = await questionsRepository.getQuestion((int)id);

            if (question == null) return NotFound();

            if (User.Identity.Name != null)
            {
                await questionsRepository.SaveOpening(User.Identity.Name, question);

                var user = await usersRepository.GetUserByUserName(User.Identity.Name);

                await notificationRepository.MarkAsSeen(user.Id, (int)id, NotificationType.QUESTION);
            }
            

            return View(question);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,Tags")] QuestionViewModel q)
        {
            var user = await questionsRepository.getUserByUserName(User.Identity.Name);
            var question = new Question();
            if (ModelState.IsValid)
            {
                question.TimeStamp = DateTime.UtcNow;
                question.Content = q.Content;
                question.Title = q.Title;
                question.User = user;
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
                await notificationRepository.AddUserToNotificationList(user.Id, AddedQuestion.Id, NotificationType.QUESTION);

                return RedirectToAction("Details", new { question.Id });
            }
            return View(q);
        }
        // Metoda koja se poziva kada se udje u detalj pitanja, kako bi sistem za preporuke ispravno radio
        public async Task SaveOpening(Question question)
        {
            await questionsRepository.SaveOpening(User.Identity.Name.ToString(), question);
        }

        [HttpPost]
        public async Task<IActionResult> AddReaction([FromForm(Name = "questionId")] string quesitonId, [FromForm(Name = "postId")] string postId, [FromForm(Name = "postType")] PostType postType, [FromForm(Name = "reactionType")] ReactionType reactionType)
        {
            var user = await questionsRepository.getUserByUserName(User.Identity.Name);

            await reactionRepository.AddReactionFromPost(user.Id, int.Parse(postId), postType, reactionType);

            return RedirectToAction("Details", new { id = int.Parse(quesitonId) });
        }


    }
}
