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
    }
}
