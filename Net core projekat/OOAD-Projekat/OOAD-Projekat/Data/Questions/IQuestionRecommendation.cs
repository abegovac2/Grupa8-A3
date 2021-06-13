using OOAD_Projekat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Questions
{
    public interface IQuestionRecommendation
    {
        public Task<ICollection<Question>> RecommendQuestions(string user);
    }
}
