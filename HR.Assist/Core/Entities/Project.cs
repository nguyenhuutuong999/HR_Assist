namespace HR.Assist.Core.Entities
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

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string ShortName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Technology { get; set; }

        [MaxLength(255)]
        public string Domain { get; set; }
        
        [Range(0, 1000)]
        public int Size { get; set; }

        public List<Team> Teams { get; set; }


    }
}
