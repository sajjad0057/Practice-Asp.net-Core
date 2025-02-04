using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Authorize(Policy = "ApiRequirementPolicy")]
        public IEnumerable<string> Get()
        {
            try
            {
                return new string[] { "dhaka", "sylhet", "khulna" };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
