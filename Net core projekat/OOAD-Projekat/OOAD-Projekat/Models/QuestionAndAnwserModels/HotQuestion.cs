using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Models.QuestionAndAnwserModels
{
    public class HotQuestion
    {
        // One to One relationship with hot questions
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
