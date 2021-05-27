using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Data;
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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyQuestions()
        {
            // Console.WriteLine(User.Identity.Name);
            return View("Index");
        }

        [HttpGet]
        public async Task<string> Pretrazi([FromQuery(Name = "searchParam")] string SearchParam)
        {
            var data = await applicationDbContext.Questions.Where(q => q.Title.ToUpper().Contains(SearchParam.ToUpper())).ToListAsync();
            data.ForEach((el) =>
            {
                Console.WriteLine(el.Title);
            });
            return SearchParam;
        }
    }
}
