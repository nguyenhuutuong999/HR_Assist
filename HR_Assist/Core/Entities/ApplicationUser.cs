using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HR_Assist.Core.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public string Avatar { get; set; }

        public string Address { get; set; }

        public string Profession { get; set; }

        public List<ApplicationUserInTeam> ApplicationUserInTeams { get; set; }
    }
}
