using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Questions
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public QuestionsRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<List<Question>> Find(String SearchParam)
        {
            var data = await applicationDbContext.Questions.Where(q => q.Title.ToUpper().Contains(SearchParam)).ToListAsync();
            return data;
        }
        public async Task<List<Question>> FindAll()
        {
            return await applicationDbContext.Questions.ToListAsync();
        }
        [Authorize]
        public async Task<List<Question>> FindMine(String UserName)
        {
            var data = await applicationDbContext.Questions.Where(q => q.User.Email == UserName).ToListAsync();
            return data;
        }
        public async Task AddQuestion(Question question)
        {
            await applicationDbContext.Questions.AddAsync(question);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Question>> ByTag(string tagName)
        {
            return await applicationDbContext.Questions
                .Join(applicationDbContext.TagPosts, q => q.Id, tp => tp.QuestionId, (q, tp) => new { question = q, tag = tp })
                .Where(qtp => qtp.tag.Tag.TagContent.ToUpper().Contains(tagName)).Select(q => q.question).ToListAsync();
        }
        

        public async Task SaveOpening(string UserName, Question question)
        {
            var user = await applicationDbContext.Users.Where(x => x.UserName == UserName).FirstOrDefaultAsync();

            var hasAllReady = await applicationDbContext.ViewedQuestionsHistory.Where(x => x.UserId == user.Id && x.QuestionId == question.Id).FirstOrDefaultAsync();

            if (hasAllReady != null) return;


            await applicationDbContext.ViewedQuestionsHistory.AddAsync(new Models.QuestionAndAnwserModels.ViewedQuestionsHistory { UserId = user.Id, QuestionId = question.Id });
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<User> getUserByUserName(string name)
        {
            return await applicationDbContext.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
        }

//todo: DeleteQuestion
        public Task DeleteQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public async Task<Question> getLastQuestion()
        {
            var maxId = applicationDbContext.Questions.Max(q => q.Id);
            return await applicationDbContext.Questions.FirstOrDefaultAsync(q => q.Id == maxId);
        }

        public Task<Question> getQuestion(int id)
        {
            return applicationDbContext.Questions
                .Include(qqq => qqq.Ratings)
                .Include(qqq => qqq.Tags)
                .ThenInclude(ttt => ttt.Tag)
                .Include(qqq => qqq.Answers)
                .ThenInclude(aaa => aaa.Ratings)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
