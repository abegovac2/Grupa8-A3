using OOAD_Projekat.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Statistics
{
    public interface IStatisticsRepository
    {
        public Task<List<SearchStatistics>> DajStatistiku(int brojSati);
        public Task EvidentirajParametarPretrage(string searchParam);
    }
}
