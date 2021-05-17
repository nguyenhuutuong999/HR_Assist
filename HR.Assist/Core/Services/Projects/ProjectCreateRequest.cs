using MediatR;
using HR.Assist.Core.Services.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace HR.Assist.Core.Services.Projects
{
    public class ProjectCreateRequest : IRequest<ResponseModel>
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public string Technology { get; set; }

        public string Domain { get; set; }

        public int Size { get; set; }
    }
}
