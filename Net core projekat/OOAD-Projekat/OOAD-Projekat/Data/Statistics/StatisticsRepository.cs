using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Statistics
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public StatisticsRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task SaveSearchTerms(string searchParam)
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
        public async Task<List<TermUsageStatistics>> GetTermUsage(int brojSati, int brojElemenata)
        {
            var trenutnoVrijeme = DateTime.Now;
            var data = await applicationDbContext.SearchStatistics.Where(ss => ss.Timestamp > trenutnoVrijeme.AddHours(-brojSati) && ss.Timestamp <= trenutnoVrijeme)
                        .GroupBy(ss => ss.Search)
                        .Select(ss => new TermUsageStatistics { Search = ss.Key, Count = ss.Count() })
                        .OrderByDescending(tus => tus.Count)
                        .Take(brojElemenata)
                        .Distinct()
                        .ToListAsync();
            return data;
        }

        public async Task<List<TagUsageStatistics>> GetTagUsage(int brojSati, int brojElemenata)
        {
            var trenutnoVrijeme = DateTime.Now;
            var data = await applicationDbContext.TagPosts.Where(tp => tp.Question.TimeStamp > trenutnoVrijeme.AddHours(-brojSati) && tp.Question.TimeStamp <= trenutnoVrijeme)
                .Select(tp => new TagUsageStatistics { Tag = tp.Tag.TagContent, Count = tp.Tag.NumOfUses })
                .OrderByDescending(tus => tus.Count)
                .Take(brojElemenata)
                .Distinct()
                .ToListAsync();
            return data;
        }
    }
}
