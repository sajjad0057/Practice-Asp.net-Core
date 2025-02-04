using System.Security.Claims;
using FirstDemo.Infrastructure.Entities.IdentityEntities;
using FirstDemo.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ILogger<TokenController> _logger;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public TokenController(
        ILogger<TokenController> logger,
        IConfiguration configuration,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService)
    {

        _logger = logger;
        _configuration = configuration;
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;

    }



    [HttpGet]
    public async Task<IActionResult> Get(string email, string password)
    {
        if (email != null && password != null)
        {
            var user = await _userManager.FindByNameAsync(email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

            if (result is not null && result.Succeeded)
            {
                var claims = (await _userManager.GetClaimsAsync(user)).ToList();

                //// By this approach if need we can passing through claims list as claim extra info in users token as per as needed.
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.UserName.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, user.Email.ToString()));

                var token = await _tokenService.GetJwtToken(claims);

                return Ok(token);
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }
        else
        {
            return BadRequest();
        }

    }
}
