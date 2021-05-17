namespace HR.Assist.Core.Entities.Contexts
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class HRAssistDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public HRAssistDbContext(DbContextOptions<HRAssistDbContext> options)
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
