using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace agilesheel.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the agilesheelUser class
    public class agilesheelUser : IdentityUser
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

    }
}
