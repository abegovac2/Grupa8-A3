using Microsoft.AspNetCore.Identity;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.ChatData
{
    public interface IChatRepository
    {
        public Task<User> GetUserByName(string name);
        public Task<List<Chat>> SearchForChat(string ChatName);
        public Task<List<Chat>> GetChatsForUser(IdentityUser user);
        public Task<Chat> GetChat(int? Id);
        public Task<List<User>> GetUsers();
        public Task<List<User>> SearchUsers(string name);
        public Task CreateNewChat(Chat chat);
        public Task DeleteChatUser(string UserId, int ChatId);
        public Task DeleteChat(int ChatId);
        public int NumberOfUsersInAChat(int ChatId);
        public Task SaveMessage(int chatId, string name, string message);
    }
}
