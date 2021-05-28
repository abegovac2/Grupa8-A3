using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.ChatData
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void CreateNewChat(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async void DeleteChat(int ChatId)
        {
            var chat = await _context.Chats.FindAsync(ChatId);
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }

        public async void DeleteChatUser(int ChatId, string UserId)
        {
            var theUser = await _context.ChatUsers.FindAsync(UserId, ChatId);
            _context.ChatUsers.Remove(theUser);
            await _context.SaveChangesAsync();
        }

        public async Task<Chat> GetChat(int? Id)
        {
            var chat = await _context.Chats
                .Include(x => x.Messages)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(m => m.Id == Id);
            chat.Messages.OrderBy(x => x.Timestamp);
            var list = new List<IdentityUser>();
            for (int i = 0; i < chat.Users.Count; ++i)
            {
                chat.Users[i].User = await _context.Users.FindAsync(chat.Users[i].UserId);
            }

            return chat;
        }

        public Task<List<Chat>> SearchForChat(string ChatName)
        {
            return _context.Chats.Where(x => x.ChatName.Contains(ChatName)).ToListAsync();
        }

        public async Task<List<Chat>> GetChatsForUser(IdentityUser user)
        {
            return await _context.Chats
                   .Include(x => x.Users)
                   .Where(x => x.Users.Any(y => y.UserId == user.Id))
                   .ToListAsync();
        }

        public Task<User> GetUserByName(string name)
        {
            return _context.Users.Where(x => x.UserName == name).FirstAsync();
        }

        public Task<List<User>> GetUsers()
        {
            return _context.Users.ToListAsync();
        }

        public int NumberOfUsersInAChat(int ChatId)
        {
            return _context.ChatUsers.Count(x => x.ChatId == ChatId);
        }

        public Task<List<User>> SearchUsers(string name)
        {
            return _context.Users.Where(x => x.UserName.Contains(name)).ToListAsync();
        }

        public void SaveMessage(int chatId, string name, string message)
        {
            var msg = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = name,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(msg);
            _context.SaveChangesAsync();
        }
    }
}
