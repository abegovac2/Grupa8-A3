﻿using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Answers
{
    public interface IAnswersRepository
    {
        public Task AddAnswer(int questionID, string content, string userID);
    }
}
