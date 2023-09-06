using CloudApper.Common.Models;
using CloudApper.Enumerations;
using CloudApper.Model;
using CloudApper.Plugin;
using DemoConnector.DependencyResolvers;
using DemoConnector.NotificationContext;
using DemoConnector.RequestValidators;
using System.Composition;
using System.Net;
using xPlugin = CloudApper.Plugin.Plugin;

namespace DemoConnector
{
    /// <summary>
    /// Gateway of your integration
    /// </summary>
    [Export(typeof(IExtension))]
    public class Gateway : IExtension
    {
        private readonly xPlugin _plugin = new();

        private readonly IRequestValidator _requestValidator;
        private readonly INotificationTypeContext _actionContext;

        /// <summary>
        /// Init your services
        /// </summary>
        public Gateway(IRequestValidator requestValidator, INotificationTypeContext actionContext)
        {
            AttributeServiceExtension.RegisterAttributeServices();
            _requestValidator = requestValidator;
            _actionContext = actionContext;
        }

        /// <summary>
        /// Run your operation as defined the action
        /// </summary>
        /// <param name="connectorSpec">Plugin configuration</param>
        /// <param name="contextData">All possible information related the triggered data</param>
        /// <param name="actionId">Action to perform</param>
        /// <param name="triggeredRecordId">Indicate which record triggered</param>
        /// <returns></returns>
        public async Task<ResponseMessage<object>> ExecuteIntendedOperationsAsync(FormPlugin connectorSpec, Dictionary<string, dynamic> contextData, string actionId = "", string triggeredRecordId = "")
        {
            // The following predefined keys contains in the dictionary:
            // - "TRIGGERED_RECORD_ID": You can get the triggeredRecordId value from dictionary like triggeredRecordId=contextData[TRIGGERED_RECORD_ID]
            // - "ACTION_ID": You can get the triggeredRecordId value from dictionary like actionId=contextData[ACTION_ID]
            // - "RELATED_RECORDS": The context data have all related records associated with triggered record. You can access like records=contextData[RELATED_RECORDS]
            // - "AVAILABLE_SCHEMAS": The context data have all related schemas associated with triggered record. You can access like schemas=contextData[AVAILABLE_SCHEMAS]
            // - "CONNECTOR_SPEC": The context data contains the workflow definition. You can access like spec=contextData[CONNECTOR_SPEC]
            // - "CreatedBy": The context data contains the user name. You can access like userName=contextData[CreatedBy]
            // - "CreatedById": The context data contains the user id. You can access like userId=contextData[CreatedById]

            //get connector data from context as you need
            List<Record> records = contextData[IntegrationConstant.Context.RELATED_RECORDS];
            string triggered_record_id = contextData[IntegrationConstant.Context.TRIGGERED_RECORD_ID];
            //string action_id = contextData[IntegrationConstant.Context.ACTION_ID];
            var triggered_record = records.FirstOrDefault(t => t.Item[IntegrationConstant.Context.ID] == triggered_record_id);
            var available_schemas = contextData[IntegrationConstant.Context.AVAILABLE_SCHEMAS];

            //validate the request
            var validationResult = _requestValidator.ValidateRequest(connectorSpec);
            if (validationResult != null && validationResult.ResponseCode != (int)HttpStatusCode.OK)
            {
                return validationResult;
            }
            //initialize the plugin
            await _plugin.InitPlugin(connectorSpec, records);

            //implement your business here
            try
            {
                //get action strategy to handle
                Models.EnumNotificationType action = (Models.EnumNotificationType)(EnumNotificationType)Enum.Parse(typeof(Models.EnumNotificationType), actionId); //get action enum from action id
                var actionStrategy = _actionContext.GetNotificationTypeStrategy(action);

                //handle the action
                var response = await actionStrategy.SendNotificationAsync(contextData, _plugin);
                response.MessageType = EnumMessageType.Success;//we should always send MessageType=Success, workflow designer will handle the ResponseCode
                return response;
            }
            catch (Exception ex)
            {
                _plugin.LogError(ex, ex.Message);
                return new ResponseMessage<object>
                {
                    MessageType = EnumMessageType.Success,//we should always send MessageType=Success, workflow designer will handle the ResponseCode
                    Message = ex.Message,
                    ResponseCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        public object ExecuteIntendedOperationsAsync(FormPlugin formPlugin, Dictionary<string, object> contextData, object sYNC_GMAILS)
        {
            throw new NotImplementedException();
        }
    }
}