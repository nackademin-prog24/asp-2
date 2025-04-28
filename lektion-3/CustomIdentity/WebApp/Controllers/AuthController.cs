using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Identity;
using WebApp.Models;

namespace WebApp.Controllers;

public class AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    #region SignUp 

    [HttpGet("signup")]
    public IActionResult SignUp(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel model, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl ?? "~/";

        if (!ModelState.IsValid)
            return View(model);

        var user = new AppUser
        {
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
            return LocalRedirect(ViewBag.ReturnUrl);

        ViewBag.ErrorMessage = "Unable to create Account";
        return View(model);
    }

    #endregion


    #region Login 

    [HttpGet("login")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl ?? "~/";

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                return LocalRedirect(ViewBag.ReturnUrl);
            }
        }

        ViewBag.ErrorMessage = "Invalid credentials";
        return View(model);
    }

    #endregion

    #region SignOut 

    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return LocalRedirect("~/");
    }

    #endregion
}
