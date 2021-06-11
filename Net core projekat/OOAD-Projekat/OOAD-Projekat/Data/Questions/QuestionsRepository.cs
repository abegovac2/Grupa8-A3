using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Data.ReactionData;
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
        private readonly IReactionRepository reactionRepository;
        public QuestionsRepository(IReactionRepository reactionRepository, ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.reactionRepository = reactionRepository;
        }
        public async Task<List<Question>> Find(String SearchParam)
        {
            var data = await applicationDbContext.Questions.Where(q => q.Title.ToUpper().Contains(SearchParam)).ToListAsync();
            data.ForEach(async x => await setupQuestionReactions(x));
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
            data.ForEach(async x => await setupQuestionReactions(x));
            return data;
        }
        public async Task AddQuestion(Question question)
        {
            await applicationDbContext.Questions.AddAsync(question);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Question>> ByTag(string tagName)
        {
            var result = await applicationDbContext.Questions
                .Join(applicationDbContext.TagPosts, q => q.Id, tp => tp.QuestionId, (q, tp) => new { question = q, tag = tp })
                .Where(qtp => qtp.tag.Tag.TagContent.ToUpper().Contains(tagName)).Select(q => q.question).ToListAsync();

            result.ForEach(async x => await setupQuestionReactions(x));

            return result;
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
        public async Task DeleteQuestion(int questionId)
        {
            await reactionRepository.DeleteReactionsForPost(questionId, PostType.QUESTION);

            var anwsers = await applicationDbContext.Answers.Where(x => x.QuestionID == questionId).ToListAsync();

            anwsers.ForEach(x =>
            {
                applicationDbContext.Answers.Remove(x);
                reactionRepository.DeleteReactionsForPost(x.Id, PostType.ANWSER);
            });

            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<Question> getLastQuestion()
        {
            var maxId = applicationDbContext.Questions.Max(q => q.Id);
            var res = await applicationDbContext.Questions.FirstOrDefaultAsync(q => q.Id == maxId);
            res.ratingCalculate = new QuestionRating();
            var react = reactionRepository.GetReactionsForPost( maxId, PostType.QUESTION);
            res.ratingCalculate.SetReactions(react);
            return res;
        }

        private async Task setupQuestionReactions(Question question)
        {
            question.ratingCalculate = new QuestionRating();
            var rating = reactionRepository.GetReactionsForPost(question.Id, PostType.QUESTION);
            question.ratingCalculate.SetReactions(rating);
        }

        private async Task setupQuestionAndAnwserReactions(Question question)
        {
            await setupQuestionReactions(question);

            question.Answers.ForEach(x =>
            {
               x.ratingCalculate = new AnwserRating();
               var ratring1 = reactionRepository.GetReactionsForPost(x.Id, PostType.ANWSER);
               x.ratingCalculate.SetReactions(ratring1);
            });
        }

        public async Task<Question> getQuestion(int id)
        {
            var result = await applicationDbContext.Questions
                .Include(qqq => qqq.Tags)
                .ThenInclude(ttt => ttt.Tag)
                .Include(qqq => qqq.Answers)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (result == null) return result;

            await setupQuestionAndAnwserReactions(result);
            
            return result;

        }

        public async Task<List<Question>> FindUnanwseredQuestions()
        {
            var result = await applicationDbContext.Questions.Where(qqq => qqq.Anwsered == false).ToListAsync();

            result.ForEach(async x => await setupQuestionReactions(x));

            return result;
        }

        public async Task UpdateQuestion(Question question)
        {
            applicationDbContext.Questions.Update(question);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Question>> getPopularQestions()
        {
            var questions = await applicationDbContext.Questions.ToListAsync();

            questions.ForEach(async x => await setupQuestionReactions(x));

            questions.Sort((x, y) => {
                var xVel = x.ratingCalculate.CalculateRating();
                var yVel = y.ratingCalculate.CalculateRating();
                if (xVel.Item1 == yVel.Item1) return xVel.Item2 - yVel.Item2;
                return xVel.Item1 - yVel.Item1;
            });

            return questions;
        }
    }
}
