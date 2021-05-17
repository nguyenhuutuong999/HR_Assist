using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR_Assist.Core.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
       
        [MaxLength(255)]
        public string Name { get; set; }

        [Range(0, 1000)]
        public int YearsOfExperience { get; set; }

        [MaxLength(255)]
        public string TechnicalSkills { get; set; }

        public bool ContractStatus { get; set; }

        public DateTime? ContractDueDate { get; set; }

        public List<ApplicationUserInTeam> ApplicationUserInTeams { get; set; }
    }
}
