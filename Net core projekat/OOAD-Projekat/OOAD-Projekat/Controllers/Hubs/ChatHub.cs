using Microsoft.AspNetCore.SignalR;
using OOAD_Projekat.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Controllers.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string chatId, string name, string text)
        {
            /*
            var context = new MessageRepository(ApplicationDbContext.NewInstance());
            var msg = context.CreateMessage(int.Parse(chatId), name, text);

            return Clients
                .Group(chatId)
                .SendAsync("ReceiveMessage",
                    msg.Name,
                    Context.ConnectionId,
                    msg.Text,
                    msg.Timestamp.ToShortTimeString()
                );*/
            return Clients
                .Group(chatId)
                .SendAsync("ReceiveMessage",
                    name,
                    Context.ConnectionId,
                    text,
                    DateTime.Now.ToShortTimeString()
                );
        }

        public Task JoinGroup(string ChatId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, ChatId);
        }
    }
}
