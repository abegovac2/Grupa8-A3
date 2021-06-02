using Microsoft.AspNetCore.Mvc;
using OOAD_Projekat.Data.Tags;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagsRepository tagsRepository;
        public TagController(ITagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<List<Tag>> GetTags([FromQuery(Name = "searchParam")] string searchParam)
        {
            if (searchParam == null) searchParam = "";
            // Normalizacija podataka
            return await tagsRepository.GetTags(searchParam.Trim().ToUpper());
        }
    }
}
