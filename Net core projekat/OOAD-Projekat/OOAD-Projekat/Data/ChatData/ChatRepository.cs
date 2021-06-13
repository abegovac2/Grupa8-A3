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

        public async Task CreateNewChat(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChat(int ChatId)
        {
            var chat = await _context.Chats
                .Include(x => x.Messages)
                .Where(x => x.Id == ChatId)
                .FirstAsync();
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChatUser(string UserId, int ChatId)
        {
            var theUser = await _context.ChatUsers.FindAsync(ChatId, UserId);
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
            var list = await _context.Chats
                   .Include(x => x.Users)
                   .Include(x => x.Messages)
                   .Where(x => x.Users.Any(y => y.UserId == user.Id))
                   .ToListAsync();

            list.ForEach(x => x.Messages.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp)));

            list.Sort((x, y) =>
            {
                if (x.Messages.Count() == 0) return 1;
                else if (y.Messages.Count() == 0) return -1;
                else
                {
                    int xVel = (x.Messages.Count() - 1);
                    int yVel = (y.Messages.Count() - 1);
                    return x.Messages[xVel].Timestamp.CompareTo(y.Messages[yVel].Timestamp) * (-1);
                }
            });

            return list;

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

        public async Task SaveMessage(int chatId, string name, string message)
        {
            var msg = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = name,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();
        }
    }
}
