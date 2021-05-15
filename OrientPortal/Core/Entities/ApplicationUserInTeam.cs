namespace HR_Assist.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ApplicationUserInTeam")] 
    public class ApplicationUserInTeam : BaseEntity
    {
        public ApplicationUserInTeam() : base()
        {

        }

        public Guid ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Guid TeamId { get; set; }

        public Team Team { get; set; }

        public DateTime? OutedDate { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
