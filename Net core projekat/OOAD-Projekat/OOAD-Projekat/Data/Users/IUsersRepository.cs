﻿using OOAD_Projekat.Models;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Users
{
    public interface IUsersRepository
    {
        public Task<User> GetUserByUserName(string name);
        public Task<bool> GetUserBlockedStatus(string name);
    }
}
