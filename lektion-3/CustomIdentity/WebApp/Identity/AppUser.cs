﻿using Microsoft.AspNetCore.Identity;

namespace WebApp.Identity;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
