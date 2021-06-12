using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.Users
{
    public class UsersRepository: IUsersRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public UsersRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<User> GetUserByUserName(string name)
        {
            return await applicationDbContext.Users.Where(x => x.UserName == name).FirstOrDefaultAsync();
        }
        public async Task<bool> GetUserBlockedStatus(string name)
        {
            return await applicationDbContext.Users.Where(x => x.UserName == name).Select(x => x.Blocked).FirstOrDefaultAsync();
        }
    }
}
