using AutoMapper;
using MediatR;
using HR_Assist.Core.Constants;
using HR_Assist.Core.Entities.Contexts;
using HR_Assist.Core.Services.Common.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using HR_Assist.Core.Entities;

namespace HR_Assist.Core.Services.Projects
{
    public class ProjectCreateHandler : IRequestHandler<ProjectCreateRequest, ResponseModel>
    {
        private readonly HR_AssistDbContext _db;
        private readonly IMapper _mapper;

        public ProjectCreateHandler(IMapper mapper, HR_AssistDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(ProjectCreateRequest request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request);

            _db.Projects.Add(project);
            await _db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = HR_AssistMessageConstants.PROJECT_CREATED_SUCCESSFULLY
            };
        }

    }
}
