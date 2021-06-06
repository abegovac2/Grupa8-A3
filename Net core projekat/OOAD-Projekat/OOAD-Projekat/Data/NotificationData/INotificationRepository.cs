using Microsoft.AspNetCore.SignalR;
using OOAD_Projekat.Controllers.Hubs;
using OOAD_Projekat.Models;
using System.Threading.Tasks;

namespace OOAD_Projekat.Data.NotificationData
{

    public interface INotificationRepository
    {
        public Task AddUserToNotificationList(string UserId, int PostId, NotificationType notificationType);
        public Task RemoveUserFromNotificationList(string UserId, int PostId, NotificationType notificationType);
        public Task RemoveAllUsersFromNotificationList(int PostId, NotificationType notificationType);
        public Task SendNotification(string UserId,int PostId, NotificationType notificationType, string Message, IHubContext<NotificationUserHub> notifyUser);
        public Task MarkAsSeen(string UserId,int PostId, NotificationType notificationType);
        public bool HasNotifications(string UserId,int PostId, NotificationType notificationType);
    }
}
