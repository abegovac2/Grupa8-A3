using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Answers
{
    public class AnswersRepository : IAnswersRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AnswersRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddAnswer(int questionID, string content, string userID)
        {
            await applicationDbContext.Answers.AddAsync(new Answer {  QuestionID = questionID, Content = content, UserId = userID, TimeStamp = DateTime.Now, AcceptedAsAnwser = false  });
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
