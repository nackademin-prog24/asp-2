﻿using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
