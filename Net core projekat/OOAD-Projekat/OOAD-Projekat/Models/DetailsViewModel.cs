using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Models
{
    public class DetailsViewModel
    {
        public Question Question { get; set; }
        // 0 like, 1 dislike
        public int QuestionReacted{ get; set; }
    }
}
