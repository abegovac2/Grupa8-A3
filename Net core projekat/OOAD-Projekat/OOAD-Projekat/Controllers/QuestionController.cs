using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Data;
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
        private readonly ApplicationDbContext applicationDbContext;
        public QuestionController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await NadjiSvaPitanja());
        }
        [Authorize]
        public async Task<IActionResult> MyQuestions()
        {
            return View("Index", await NadjiMojaPitanja(User.Identity.Name));
        }

        [HttpGet]
        public async Task<IActionResult> Pretrazi([FromQuery(Name = "searchParam")] string SearchParam)
        {
            return View("Index", await NadjiPitanja(SearchParam));
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

        private async Task<List<Question>> NadjiPitanja(String SearchParam)
        {
            var data = await applicationDbContext.Questions.Where(q => q.Title.ToUpper().Contains(SearchParam)).ToListAsync();
            return data;
        }
        private async Task<List<Question>> NadjiSvaPitanja()
        {
            return await applicationDbContext.Questions.ToListAsync();
        }
        [Authorize]
        private async Task<List<Question>> NadjiMojaPitanja(String UserName)
        {
            var data = await applicationDbContext.Questions.Where(q => q.User.Email == UserName).ToListAsync();
            return data;
        }
    }
}
