using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Models.QuestionAndAnwserModels
{
    public class ViewedQuestionsHistory
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
