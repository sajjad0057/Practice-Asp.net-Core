using CloudApper.Common.Models;
using CloudApper.Model;

namespace DemoConnector.IServices
{
    /// <summary>
    /// Newspaper services
    /// </summary>
    public interface ISMSSenderService
    {
        /// <summary>
        /// Newspaper save service
        /// </summary>
        /// <param name="connectorSpec"></param>
        /// <returns></returns>
        Task<ResponseMessage<object>> SendSMSAsync(FormPlugin connectorSpec);
    }
}