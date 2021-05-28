using OOAD_Projekat.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Statistics
{
    public interface IStatisticsRepository
    {
        public Task<List<TermUsageStatistics>> GetTermUsage(int brojSati, int brojElemenata);

        public Task SaveSearchTerms(string searchParam);

        public Task<List<TagUsageStatistics>> GetTagUsage(int brojSati, int brojElemenata);
    }
}
