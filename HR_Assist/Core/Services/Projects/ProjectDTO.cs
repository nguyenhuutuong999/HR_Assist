using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Assist.Core.Services.Projects
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    
        public string ShortName { get; set; }
       
        public string Description { get; set; }

        public string Technology { get; set; }

        public string Domain { get; set; }

        public int Size { get; set; }


    }
}
