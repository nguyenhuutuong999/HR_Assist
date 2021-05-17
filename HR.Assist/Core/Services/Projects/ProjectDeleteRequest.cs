namespace HR.Assist.Core.Services.Projects
{
    using System;
    using MediatR;
    using HR.Assist.Core.Services.Common.Models;

    public class ProjectDeleteRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}
