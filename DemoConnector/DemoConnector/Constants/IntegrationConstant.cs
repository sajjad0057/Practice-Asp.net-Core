namespace DemoConnector
{
    /// <summary>
    /// String constants
    /// </summary>
    public static class IntegrationConstant
    {
        /// <summary>
        /// Constants defined in ContextData dictionary
        /// </summary>
        public static class Context
        {
            /// <summary>
            /// Plugin configuration in ContextData dictionary
            /// </summary>
            public const string CONNECTOR_SPEC = "CONNECTOR_SPEC";

            /// <summary>
            /// Triggered record id in ContextData dictionary
            /// </summary>
            public const string TRIGGERED_RECORD_ID = "TRIGGERED_RECORD_ID";

            /// <summary>
            /// Action id in ContextData dictionary
            /// </summary>
            public const string ACTION_ID = "ACTION_ID";

            /// <summary>
            /// Trigger record in ContextData dictionary
            /// </summary>
            public const string TRIGGERED_RECORD = "TRIGGERED_RECORD";

            /// <summary>
            /// Related records in ContextData dictionary
            /// </summary>
            public const string RELATED_RECORDS = "RELATED_RECORDS";

            /// <summary>
            /// Available schemas/forms in ContextData dictionary
            /// </summary>
            public const string AVAILABLE_SCHEMAS = "AVAILABLE_SCHEMAS";

            /// <summary>
            /// Triggered record id in ContextData dictionary
            /// </summary>
            public const string ID = "id";
        }

        /// <summary>
        /// Development environment settings
        /// </summary>
        public static class DevelopmentEnvironment
        {
            /// <summary>
            /// ASPNETCORE_ENVIRONMENT like Development,Staging,Production
            /// </summary>
            public const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";

            /// <summary>
            /// Testing CloudApper API base URL
            /// </summary>
            public const string API_BASE_URL = "API_BASE_URL";

            /// <summary>
            /// Access token to access the API
            /// </summary>
            public const string ACCESS_TOKEN = "ACCESS_TOKEN";
        }
    }
}