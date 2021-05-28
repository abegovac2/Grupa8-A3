using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Data;
using OOAD_Projekat.Data.Statistics;
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
        private readonly IStatisticsRepository statisticsRepository;
        public StatisticController(IStatisticsRepository statisticsRepository)
        {
            this.statisticsRepository = statisticsRepository;
        }
        public IActionResult Index()
        {
            return View(DajStatistiku(24));
        }


        private async Task<List<SearchStatistics>> DajStatistiku(int brojSati)
        {
            return await statisticsRepository.DajStatistiku(brojSati);
        }
        [HttpPost]
        public async Task EvidentirajParametarPretrage([FromForm(Name = "searchParam")] string searchParam)
        {
            await statisticsRepository.EvidentirajParametarPretrage(searchParam);
        }
    }
}
