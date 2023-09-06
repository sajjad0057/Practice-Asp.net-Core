using CloudApper.Common.Models;
using DemoConnector.Models;

using xPlugin = CloudApper.Plugin.Plugin;

namespace DemoConnector.Strategies
{
    /// <summary>
    /// Action strategy
    /// </summary>
    public interface INotificationStrategy
    {
        /// <summary>
        /// Handle the action
        /// </summary>
        /// <param name="context"></param>
        /// <param name="plugin"></param>
        /// <returns></returns>
        Task<ResponseMessage<dynamic>> SendNotificationAsync(Dictionary<string, dynamic> context, xPlugin plugin);
        /// <summary>
        /// Target Action to handle
        /// </summary>
        EnumNotificationType NotificationType { get; }
    }
}