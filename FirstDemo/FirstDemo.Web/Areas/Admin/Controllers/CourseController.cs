using FirstDemo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CourseController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        CourseCreateModel model = new CourseCreateModel();
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseCreateModel model)
    {
        if (ModelState.IsValid)
            await model.CreateCourseAsync();

        return View(model);
    }

}
