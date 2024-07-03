using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProgettoAPPWEB24.Models;

namespace ProgettoAPPWEB24.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ProgettoAPPWEB24User class
public class ProgettoAPPWEB24User : IdentityUser
{
    public string Role { get; set; } = "User";
}

