using System;
using System.ComponentModel.DataAnnotations;

namespace OOAD_Projekat.Models.Statistics
{
    public class SearchStatistics
    {
        [Key]
        public int Id { get; set; }

        public string Search { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
