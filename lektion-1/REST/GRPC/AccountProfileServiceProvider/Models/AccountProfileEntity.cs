﻿using System.ComponentModel.DataAnnotations;

namespace AccountProfileServiceProvider.Models;

public class AccountProfileEntity
{
    [Key]
    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}
