using MediatR;
using HR_Assist.Core.Services.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace HR_Assist.Core.Services.Projects
{
    public class ProjectCreateRequest : IRequest<ResponseModel>
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public string Domain { get; set; }
    }
}
