﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OOAD_Projekat.Controllers.Hubs;
using OOAD_Projekat.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.NotificationData
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly ApplicationDbContext _context;

        private readonly IUserConnectionManager _userConnectionManager;

        public NotificationRepository(ApplicationDbContext context, IUserConnectionManager userConnectionManager)
        {
            _context = context;
            _userConnectionManager = userConnectionManager;
        }

        //Notification user control

        public async Task AddUserToNotificationList(string UserId, int PostId, NotificationType notificationType)
        {
            var notifyUser = new NotifyUser
            {
                UserId = UserId,
                PostId = PostId,
                NotificationType = notificationType
            };

            _context.NotifyUsers.Add(notifyUser);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromNotificationList(string UserId, int PostId, NotificationType notificationType)
        {
            var notifyUser = await _context.NotifyUsers.Where(x => x.UserId == UserId && x.PostId == PostId && x.NotificationType == notificationType).ToListAsync();

            /*new NotifyUser
        {
            UserId = UserId,
            PostId = PostId,
            NotificationType = notificationType
        };*/

            if (notifyUser.Count() == 1)
            {
                _context.NotifyUsers.Remove(notifyUser[0]);
                await _context.SaveChangesAsync();
            }
        }

        //Notification control

        public async Task MarkAsSeen(string UserId, int PostId, NotificationType notificationType)
        {
            var notifications = await _context.Notifications.Where(x => x.UserId == UserId && x.PostId == PostId && x.NotificationType == notificationType).ToListAsync();

            notifications.ForEach(
                x =>
                {
                    x.Seen = true;
                    _context.Notifications.Update(x);
                });

            await _context.SaveChangesAsync();
        }

        public async Task SendNotification(string UserId, int PostId, NotificationType notificationType, string Message, [FromServices] IHubContext<NotificationUserHub> notifyUser)
        {

            var usersToNotify = await _context.NotifyUsers.Where(x => x.UserId != UserId && x.PostId == PostId && x.NotificationType == notificationType).ToListAsync();

            usersToNotify.ForEach(
                x =>
                {
                    var notif = new Notification
                    {
                        UserId = x.UserId,
                        PostId = PostId,
                        NotificationType = notificationType,
                        Message = Message,
                        Seen = false
                    };

                    _context.Notifications.Add(notif);

                    //signalR notification

                    var connections = _userConnectionManager.GetUserConnections(x.UserId);

                    foreach (var connection in connections)
                    {
                        notifyUser.Clients.Client(connection)
                        .SendAsync("NotifyUser", notif.PostId, notif.NotificationType, notif.Message);
                    }

                }
            );

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAllUsersFromNotificationList(int PostId, NotificationType notificationType)
        {
            var removeAllUsers = await _context.NotifyUsers.Where(x => x.PostId == PostId && x.NotificationType == notificationType).ToListAsync();

            removeAllUsers.ForEach(notifyUser => _context.NotifyUsers.Remove(notifyUser));

            await _context.SaveChangesAsync();
        }

        public bool HasNotifications(string UserId, int PostId, NotificationType notificationType)
        {
            var hasNotifications = _context.Notifications.Where(x => x.UserId == UserId && x.PostId == PostId && x.NotificationType == notificationType && !x.Seen).ToList();

            return (hasNotifications.Count() != 0);
        }
    }
}
