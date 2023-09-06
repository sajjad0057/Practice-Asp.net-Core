using DemoConnector.IServices;

namespace DemoConncetor.Test.NewspaperServiceTest
{
    /// <summary>
    /// News service test
    /// </summary>
    public class EmailSenderServiceTest : TestBase
    {
        private readonly IEmailSenderService _emailSenderService;

        /// <summary>
        /// Init the news service
        /// </summary>
        public EmailSenderServiceTest()
        {
            _emailSenderService = GetService<IEmailSenderService>();
        }

        /// <summary>
        /// Test the news get method
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Success_SendEmailAsync()
        {
            //Arrange


            //Act
            var testResult = await _emailSenderService.SendEmailAsync(null);

            //Assert
            Assert.NotNull(testResult);
            Assert.True(testResult.Success);
            Assert.Equal(200, testResult.ResponseCode);
        }
    }
}