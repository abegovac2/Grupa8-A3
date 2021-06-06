using System.Collections.Generic;

namespace OOAD_Projekat.Data.NotificationData
{
    public interface IUserConnectionManager
    {
        void KeepUserConnection(string userId, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnections(string userId);
    }

}
