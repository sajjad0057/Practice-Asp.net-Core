using CloudApper.Common.Models;
using DemoConnector.IServices;
using DemoConnector.Models;
using DemoConnector.ServiceAttributes;
using DemoConnector.Strategies;

using xPlugin = CloudApper.Plugin.Plugin;

namespace DemoConnector.ConcreteStrategies
{
    /// <summary>
    /// News save business
    /// </summary>
    /// 
    [ScopedService]
    public class SMSSendStrategy : INotificationStrategy
    {
        /// <summary>
        /// Action of news get
        /// </summary>
        public EnumNotificationType NotificationType => EnumNotificationType.SMS;

        private readonly ISMSSenderService _smsSenderService;
        /// <summary>
        /// Init EmailSenderService
        /// </summary>
        /// <param name="smsSenderService"></param>
        public SMSSendStrategy(ISMSSenderService smsSenderService)
        {
            _smsSenderService = smsSenderService;
        }

        /// <summary>
        /// Email send business implementation
        /// </summary>
        /// <param name="context">All possible information related the triggered data</param>
        /// <param name="plugin">Base plugin to access the sdk operation</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseMessage<dynamic>> SendNotificationAsync(Dictionary<string, dynamic> context, xPlugin plugin)
        {
            //implement your get news related business here
            return await _smsSenderService.SendSMSAsync(null);
        }
    }
}
