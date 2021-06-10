using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Questions
{
    public interface IQuestionsRepository
    {
        public Task<List<Question>> Find(String SearchParam);
        public Task<List<Question>> FindAll();
        public Task<List<Question>> FindMine(String UserName);
        public Task AddQuestion(Question question);
        public Task DeleteQuestion(int questionId);
        public Task<Question> getLastQuestion();
        public Task<Question> getQuestion(int id);
        public Task<List<Question>> ByTag(String tagName);
        public Task SaveOpening(string UserName, Question question);
        public Task<User> getUserByUserName(string name);
        public Task<List<Question>> FindUnanwseredQuestions();
        Task UpdateQuestion(Question question);
    }
}
