using CloudApper.Common.Models;
using CloudApper.Model;

namespace DemoConnector.IServices
{
    /// <summary>
    /// Newspaper services
    /// </summary>
    public interface IEmailSenderService
    {
        /// <summary>
        /// Newspaper save service
        /// </summary>
        /// <param name="connectorSpec"></param>
        /// <returns></returns>
        Task<ResponseMessage<object>> SendEmailAsync(FormPlugin connectorSpec);
    }
}