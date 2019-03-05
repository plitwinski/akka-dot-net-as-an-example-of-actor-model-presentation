using System;
using System.Threading.Tasks;

namespace Multi.Tenant.Apis
{
    public static class NotificationApi
    {
        public static async Task<Notification> GetNotificationAsync(string userId)
        {
            await Task.Delay(5000);
            return new Notification($"New notification {Guid.NewGuid().ToString()} for user id {userId}");
        }
    }
}
