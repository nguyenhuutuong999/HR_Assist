namespace HR_Assist.Core.Entities.Contexts
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class HR_AssistDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public HR_AssistDbContext(DbContextOptions<HR_AssistDbContext> options)
            : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<ApplicationUserInTeam> ApplicationUseInTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
        }
    }
}
