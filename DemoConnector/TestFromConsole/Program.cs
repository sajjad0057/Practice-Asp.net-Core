using CloudApper.Model;
using DemoConnector;
using DemoConnector.ConcreteStrategies;
using DemoConnector.NotificationContext;
using DemoConnector.RequestValidators;
using DemoConnector.Services;
using DemoConnector.Strategies;
using Newtonsoft.Json;
using System.Reflection;

try
{
    //dynamic? formPluginForGetDepartments = null;
    Dictionary<string, object> contextData = new();
    Record record = new();
    //dynamic? response = null;

    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, @"Data\sample.json");
    FormPlugin formPlugin = JsonConvert.DeserializeObject<FormPlugin>(File.ReadAllText(path))!;
    IEnumerable<INotificationStrategy> strategys = new List<INotificationStrategy>()
    {
        new SMSSendStrategy(new SMSSenderService())
    };
    new Gateway().ExecuteIntendedOperationsAsync(formPlugin, contextData, "SMS").Wait();
}
catch(Exception ex)
{

}
