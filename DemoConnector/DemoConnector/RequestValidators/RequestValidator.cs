using CloudApper.Common.Models;
using CloudApper.Enumerations;
using CloudApper.Model;
using DemoConnector.ServiceAttributes;
using System.Net;

namespace DemoConnector.RequestValidators
{
    /// <summary>
    /// Request validator
    /// </summary>
    /// 
    [ScopedService]
    public class RequestValidator : IRequestValidator
    {
        /// <summary>
        /// Validate the request
        /// </summary>
        /// <param name="connectorSpec">Plugin configuration</param>
        /// <returns></returns>
        public ResponseMessage<object> ValidateRequest(FormPlugin connectorSpec)
        {
            var response = new ResponseMessage<object>
            {
                Success = true,
                Message = "Request is valid.",
                ResponseCode = (int)HttpStatusCode.OK
            };
            //check if configuration is empty then throw bad request
            if (string.IsNullOrWhiteSpace(connectorSpec?.PluginConfiguration))
            {
                response.Success = false;
                response.MessageType = EnumMessageType.Success;//we should always send MessageType=Success, workflow designer will handle the ResponseCode
                response.Message = "Configuration is empty.";
                response.ResponseCode = (int)HttpStatusCode.BadRequest;
            }
            //add more validation here
            return response;
        }
    }
}
