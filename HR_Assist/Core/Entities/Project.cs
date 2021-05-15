namespace HR_Assist.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Project")]
    public class Project : BaseEntity
    {
        public Project() : base()
        {

        }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string ShortName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Domain { get; set; }

        public List<Team> Team { get; set; }


    }
}
