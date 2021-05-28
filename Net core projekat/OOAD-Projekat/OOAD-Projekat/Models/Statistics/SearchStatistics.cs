using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
