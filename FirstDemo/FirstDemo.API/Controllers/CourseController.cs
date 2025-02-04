using Autofac;
using FirstDemo.API.Models;
using FirstDemo.Infrastructure.BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstDemo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ILifetimeScope _scope;
    private readonly ILogger<CourseController> _logger;

    public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
    }

    [HttpGet, Authorize(Policy = "CourseViewRequirementPolicy")]
    public async Task<IEnumerable<Course>?> Get()
    {
        try
        {
            var model = _scope.Resolve<CourseModel>();
            return await model.GetCoursesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Couldn't get courses");
            return null;
        }
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "CourseViewRequirementPolicy")]
    public async Task<Course?> Get(Guid id)
    {
        try
        {
            var model = _scope.Resolve<CourseModel>();
            return await model.GetCourseByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Couldn't get courses");
            return null;
        }
    }

    [HttpPost()]
    //[Authorize(Policy = "CourseViewRequirementPolicy")]
    public async Task<IActionResult> Post([FromBody] CourseModel model)
    {
        try
        {
            model.ResolveDependency(_scope);
            await model.CreateCourseAsync();

            return Created();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Couldn't create course");
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CourseModel model)
    {
        try
        {
            model.ResolveDependency(_scope);
            await model.UpdateCourseAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Couldn't update course");
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var model = _scope.Resolve<CourseModel>();
            await model.DeleteCourseAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Couldn't delete course");
            return BadRequest();
        }
    }

}
