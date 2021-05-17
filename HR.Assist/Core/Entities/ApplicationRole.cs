using Microsoft.AspNetCore.Identity;
using System;

namespace HR.Assist.Core.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base()
        {

        }

        public ApplicationRole(string rolename) : base(rolename)
        {

        }
    }
}
