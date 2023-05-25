using Microsoft.AspNetCore.Mvc;

namespace GlobalExceptionHandling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            //int x = 0;
            //var res = 5 / x;

            await Task.CompletedTask;

            var obj = new
            {
                name = "sajjad",
                gender = "male",
                contact = new
                {
                    mobile = 01774135869,
                    mail = "sajjad@gmail.com"
                },
                arr  = new[] {1,3,4,5,6,7,8,9,10,11,12}
            };

            _logger.LogInformation($"obj : {obj}");
            _logger.LogInformation("obj values :  {@obj} : ", obj);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}