using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Autofac;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using FirstDemo.Infrastructure.Entities.IdentityEntities;
using System.Security.Claims;

namespace FirstDemo.Web.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IEmailSender _emailSender;
    private readonly ILifetimeScope _scope;

    public AccountController(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<AccountController> logger,
        ILifetimeScope scope)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _scope = scope;
    }

    public async Task<IActionResult> Register(string? returnUrl = null)
    {
        var model = _scope.Resolve<RegisterModel>();
        model.ReturnUrl = returnUrl;
        model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
       
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        model.ReturnUrl ??= Url.Content("~/");
        model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            //await _roleManager.CreateAsync(new ApplicationRole("Admin"));
            //await _roleManager.CreateAsync(new ApplicationRole("Teacher"));

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                //await _userManager.AddToRolesAsync(user, new string[] { "Admin" });

                //// add claims to user when registering new user - 
                //// Claim type: "ViewCourseClaim", Claim value: "true"
                await _userManager.AddClaimsAsync(user, new Claim[]
                {
                        new Claim("ViewCourseClaim", "true"),
                        new Claim("CourseDeletClaim", "true")
                });

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                    values: new { area = "", userId = user.Id, code = code, returnUrl = model.ReturnUrl },
                    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl });
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(model.ReturnUrl);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }

    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        var model = _scope.Resolve<LoginModel>();

        returnUrl ??= Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        model.ReturnUrl = returnUrl;

        return View(model);
    }

    [HttpPost,ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {
        model.ReturnUrl ??= Url.Content("~/");

        model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect(model.ReturnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout(string? returnUrl = null)
    {
        await _signInManager.SignOutAsync();
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home"); ;
        }
    }

    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
