
using DemoConnector.Models;
using DemoConnector.Strategies;

namespace DemoConnector.NotificationContext
{
    /// <summary>
    /// Interface to access strategy
    /// </summary>
    public interface INotificationTypeContext
    {
        /// <summary>
        /// Get target strategy
        /// </summary>
        /// <param name="notificationType"></param>
        /// <returns></returns>
        INotificationStrategy GetNotificationTypeStrategy(EnumNotificationType notificationType);
    }
}