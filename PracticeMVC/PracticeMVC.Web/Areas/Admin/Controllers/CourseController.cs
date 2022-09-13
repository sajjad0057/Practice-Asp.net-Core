using Microsoft.AspNetCore.Mvc;

namespace PracticeMVC.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }
    }
}
