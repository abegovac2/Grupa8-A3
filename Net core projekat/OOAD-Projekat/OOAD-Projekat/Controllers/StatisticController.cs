using Microsoft.AspNetCore.Mvc;
using OOAD_Projekat.Data.Statistics;
using OOAD_Projekat.Models.Statistics;
using System.Collections.Generic;
using System.Threading.Tasks;

// TODO View za statistiku :)
namespace OOAD_Projekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticsRepository statisticsRepository;
        public StatisticController(IStatisticsRepository statisticsRepository)
        {
            this.statisticsRepository = statisticsRepository;
        }

        [HttpPost]
        public async Task SaveSearchTerms([FromForm(Name = "searchParam")] string searchParam)
        {
            await statisticsRepository.SaveSearchTerms(searchParam);
        }
        [HttpGet("termUsage")]
        public async Task<List<TermUsageStatistics>> GetTermUsage([FromQuery(Name = "hours")] int brojSati, [FromQuery(Name = "elements")] int brojElemenata)
        {
            return await statisticsRepository.GetTermUsage(brojSati, brojElemenata);
        }
        [HttpGet("tagUsage")]
        public async Task<List<TagUsageStatistics>> GetTagUsage([FromQuery(Name = "hours")] int brojSati, [FromQuery(Name = "elements")] int brojElemenata)
        {
            return await statisticsRepository.GetTagUsage(brojSati, brojElemenata);
        }
    }
}
