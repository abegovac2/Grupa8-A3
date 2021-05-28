using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Data;
using OOAD_Projekat.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// TODO View za statistiku :)
namespace OOAD_Projekat.Controllers
{
    public class StatisticController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public StatisticController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View(DajStatistiku(24));
        }


        private async Task<List<SearchStatistics>> DajStatistiku(int brojSati)
        {
            var trenutnoVrijeme = DateTime.Now;
            var data = await applicationDbContext.SearchStatistics.Where(ss => ss.Timestamp > trenutnoVrijeme.AddHours(brojSati) && ss.Timestamp <= trenutnoVrijeme).ToListAsync();
            return data;
        }
        [HttpPost]
        public async Task EvidentirajParametarPretrage([FromForm(Name = "searchParam")] string searchParam)
        {
            var currentTimestamp = DateTime.Now;
            var rijeci = Regex.Matches(searchParam, @"[^\W\d][\w'-]*(?<=\w)").ToList();

            foreach (var rijec in rijeci)
            {
                var searchStatistics = new SearchStatistics { Search = rijec.ToString(), Timestamp = currentTimestamp };
                await applicationDbContext.SearchStatistics.AddAsync(searchStatistics);
            }
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
