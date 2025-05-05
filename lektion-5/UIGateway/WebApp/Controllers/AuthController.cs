using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers;

public class AuthController(AuthService authService, SignInManager<User> signInManager) : Controller
{
    private readonly AuthService _authService = authService;
    private readonly SignInManager<User> _signInManager = signInManager;

    #region Local Identity

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }


        var result = await _authService.SignUpAsync(model.Email, model.Password);
        if (!result.Succeeded)
        {
            ViewBag.ErrorMessage(result.Message);
            return View(model);
        }


        return RedirectToAction("Login", "Auth");
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = "/")
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "/")
    {
        ViewBag.ReturnUrl = returnUrl;

        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(model.Email, model.Password);
            if (result.Succeeded)
                return LocalRedirect(returnUrl);
        }

        ViewBag.ErrorMessage("Invalid email or password");
        return View(model);
    }

    #endregion

    [HttpPost]
    public IActionResult ExternalLogin(string provider, string returnUrl = "/")
    {
        ViewBag.ReturnUrl = returnUrl;

        var redirectUri = Url.Action("ExternalLoginCallback", "Auth", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);
        return Challenge(properties, provider);
    }

    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/", string remoteError = null!)
    {
        ViewBag.ReturnUrl = returnUrl;

        if (!string.IsNullOrEmpty(remoteError))
        {
            ViewBag.ErrorMessage = remoteError;
            return View("Login");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            return RedirectToAction("Login");

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (result.Succeeded)
            return LocalRedirect(returnUrl);

        string email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
        string username = $"{info.LoginProvider.ToLower()}_{email}";
        string firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!;
        string lastName = info.Principal?.FindFirstValue(ClaimTypes.Surname)!;

        var authResult = await _authService.SignUpExternalAsync(email, info);
        if (authResult.Succeeded)
        {
            //using var http = new HttpClient();
            //var response = await http.PostAsJsonAsync("", new { userId = .. , firstName = firstName, lastName = lastName });

            return LocalRedirect(returnUrl);
        }

        return RedirectToAction("Login");
    }

}
