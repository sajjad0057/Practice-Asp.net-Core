using CloudApper.Common.Models;
using CloudApper.Model;

namespace DemoConnector.RequestValidators
{
    /// <summary>
    /// Request validator interface
    /// </summary>
    public interface IRequestValidator
    {
        /// <summary>
        /// Validate request
        /// </summary>
        /// <param name="connectorSpec">Plugin configuration</param>
        /// <returns></returns>
        ResponseMessage<object> ValidateRequest(FormPlugin connectorSpec);
    }
}
