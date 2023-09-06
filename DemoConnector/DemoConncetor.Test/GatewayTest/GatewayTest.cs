using CloudApper.Model;
using CloudApper.Plugin;
using DemoConnector;
using Newtonsoft.Json;
using Record = CloudApper.Model.Record;

namespace DemoConncetor.Test.GatewayTest
{
    /// <summary>
    /// Gateway test
    /// </summary>
    public class GatewayTest : TestBase
    {
        private readonly IExtension _extension;
        /// <summary>
        /// Init the gateway interface
        /// </summary>
        public GatewayTest()
        {
            _extension = GetService<IExtension>();
        }

        /// <summary>
        /// Test the gateway action
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Success_ExecuteIntendedOperationsAsync()
        {
            try
            {
                //Arrange
                FormPlugin formPlugin = JsonConvert.DeserializeObject<FormPlugin>(File.ReadAllText("GatewayTest\\test-data\\configuration.json"));
                Dictionary<string, dynamic> contextData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(File.ReadAllText("GatewayTest\\test-data\\context-data.json"));
                contextData[IntegrationConstant.Context.RELATED_RECORDS] = contextData[IntegrationConstant.Context.RELATED_RECORDS].ToObject<List<Record>>();

                //Act
                var testResult = await _extension.ExecuteIntendedOperationsAsync(formPlugin, contextData, "SMS", "8a68408e-3652-4488-a6ed-2f4a03ea995b");

                //Assert
                Assert.NotNull(testResult);
                Assert.True(testResult.Success);
                Assert.Equal(200, testResult.ResponseCode);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}