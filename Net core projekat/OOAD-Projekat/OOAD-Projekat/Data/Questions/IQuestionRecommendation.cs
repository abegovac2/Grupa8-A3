using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Questions
{
    public interface IQuestionRecommendation
    {
        public Task<ICollection<Question>> RecommendQuestions(string user);
    }
}
