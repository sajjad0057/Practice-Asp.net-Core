using CloudApper.Plugin;
using DemoConnector;
using DemoConnector.DependencyResolvers;
using Microsoft.Extensions.DependencyInjection;

namespace DemoConncetor.Test
{
    /// <summary>
    /// Test base class to init the test environment
    /// </summary>
    public class TestBase
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Init the test environment
        /// </summary>
        public TestBase()
        {
            //Set the development environment
            Environment.SetEnvironmentVariable(IntegrationConstant.DevelopmentEnvironment.ASPNETCORE_ENVIRONMENT, "Development");
            Environment.SetEnvironmentVariable(IntegrationConstant.DevelopmentEnvironment.API_BASE_URL, "https://your-api.com");
            Environment.SetEnvironmentVariable(IntegrationConstant.DevelopmentEnvironment.ACCESS_TOKEN, "");

            //initialize services
            ServiceCollection serviceCollection = AttributeServiceExtension.RegisterAttributeServices();
            serviceCollection.AddScoped<IExtension, Gateway>();
            serviceCollection.AddScoped<Plugin>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
        /// <summary>
        /// Get the target service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
