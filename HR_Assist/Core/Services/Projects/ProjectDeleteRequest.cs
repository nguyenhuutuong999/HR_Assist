namespace HR_Assist.Core.Services.Projects
{
    using System;
    using MediatR;
    using HR_Assist.Core.Services.Common.Models;

    public class ProjectDeleteRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}
