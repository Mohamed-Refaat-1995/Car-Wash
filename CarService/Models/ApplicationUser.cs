﻿using Microsoft.AspNetCore.Identity;

namespace CarService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Address { get; set; }

        public string? Pinding { get; set; }




    }
}
