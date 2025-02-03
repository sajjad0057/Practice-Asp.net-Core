using Autofac;
using FirstDemo.Infrastructure.Exceptions;
using FirstDemo.Web.Areas.Admin.Models;
using FirstDemo.Web.Codes;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CourseController : Controller
{
    private readonly ILifetimeScope _scope;
    private readonly ILogger<CourseController> _logger;

    public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
    }

    [Authorize(Policy = "CourseViewRequirementPolicy")]
    public IActionResult Index()
    {
        return View();
    }

    public JsonResult GetCourseData()
    {
        var dataTableModel = new DataTablesAjaxRequestModel(Request);  //// Here Request - object is Controller class property.
        var model = _scope.Resolve<CourseListModel>();
        return Json(model.GetPagedCourses(dataTableModel));

    }

    [Authorize(Policy = "CourseViewRequirementPolicy")]
    public IActionResult Create()
    {
        CourseCreateModel model = _scope.Resolve<CourseCreateModel>();
        return View(model);
    }

    [Authorize(Policy = "CourseManagementPolicy")]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseCreateModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            try
            {
                await model.CreateCourseAsync();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully added a new course .",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch (DuplicateException ioe)
            {
                _logger.LogError(ioe, ioe.Message);

                //for showing duplicateException Message in Client Side Validation Message Showing Section.
                ModelState.AddModelError("", ioe.Message);


                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = ioe.Message,
                    Type = ResponseTypes.Warning
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There Was a Problem in Creating Course .",
                    Type = ResponseTypes.Danger
                });
            }

        }
        else
        {
            ////// For showing validation message from server side for invalid ModelState , and Messages -   
            string messageText = string.Empty;
            foreach (var message in ModelState.Values)
            {
                for (int i = 0; i < message.Errors.Count(); i++)
                {
                    messageText += $"{message.Errors[i].ErrorMessage}";


                }

            }
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = messageText,
                Type = ResponseTypes.Warning
            });
        }

        return View(model);
    }


    [Authorize(Policy = "CourseManagementPolicy")]
    public IActionResult Edit(Guid id)
    {
        CourseEditModel model = _scope.Resolve<CourseEditModel>();
        model.LoadData(id);
        return View(model);
    }

    [Authorize(Policy = "CourseManagementPolicy")]
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(CourseEditModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);
            try
            {
                model.EditCourse();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully Updated course ! ",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There Was a Problem in Updating Course .",
                    Type = ResponseTypes.Danger
                });

            }
        }

        return View(model);
    }


    [Authorize(Policy = "CourseDeletePolicy")]
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var model = _scope.Resolve<CourseListModel>();
            model.DeleteCourse(id);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Successfully Deleted this course ! ",
                Type = ResponseTypes.Success
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "There Was a Problem in Deleting Course .",
                Type = ResponseTypes.Danger
            });
        }
        return RedirectToAction("Index");
    }
}
