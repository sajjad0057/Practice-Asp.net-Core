using System.Diagnostics;
using System.Text.Json;
using FirstDemo.Infrastructure.Services;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseModel _courseModel;
        private readonly IDataUtility _dataUtility;
        private readonly ITimeService _timeService;

        public HomeController(ILogger<HomeController> logger, ICourseModel courseModel, IDataUtility dataUtility, ITimeService timeService)
        {
            _logger = logger;
            _courseModel = courseModel;
            _dataUtility = dataUtility;
            _timeService = timeService;
        }

        public async Task<IActionResult> IndexAsync(string id)
        {
            _logger.LogInformation("I'm in Index page !");
            ViewData["id"] = id;

            //// string sql = "delete from courses where Title = 'ADO.NET' ";
            //string sql = $"insert into Courses (Id,Title,Fees,ClassStartDate) values(@xId,@xTitle,@xFees,@xClassStartDate)";

            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("xId", Guid.NewGuid());
            //parameters.Add("xTitle", "ADO.NET");
            //parameters.Add("xFees", 3000);
            //parameters.Add("xClassStartDate", _timeService.Now.AddDays(30).ToString());
            //// Test for Ado.Net - 
            //await _dataUtility.ExecuteCommandAsync(sql, parameters, System.Data.CommandType.StoredProcedure);


            //string sql = "select * from Courses";
            string sql = "GetCourses";   //// Here , "GetCourses" is Stored Procedure name, that position in Database StoredProcedure folder - 

            var data = await _dataUtility.GetDataAsync(sql, null, System.Data.CommandType.StoredProcedure);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            var model = new TestModel();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
