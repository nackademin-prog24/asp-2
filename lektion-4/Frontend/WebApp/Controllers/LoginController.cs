using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Login;

namespace WebApp.Controllers;

public class LoginController(SignInManager<AccountUser> signInManager) : Controller
{
    private readonly SignInManager<AccountUser> _signInManager = signInManager;

    public IActionResult Index(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model, string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;

        if(ModelState.IsValid)
        {
            var response = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (response.Succeeded)
            {
                return LocalRedirect(ViewBag.ReturnUrl);
            }
        }

        ViewBag.ErrorMessage = "Invalid email or password";
        return View(model);
    }
}
