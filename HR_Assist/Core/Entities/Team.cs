namespace HR_Assist.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Team")]
    public class Team : BaseEntity
    {
        public Team() : base()
        {

        }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string ShortName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public int TeamSize { get; set; }

        public Guid? ProjectId { get; set; }

        public Project Project { get; set; }


        public List<ApplicationUserInTeam> ApplicationUserInTeams { get; set; }

    }
}
