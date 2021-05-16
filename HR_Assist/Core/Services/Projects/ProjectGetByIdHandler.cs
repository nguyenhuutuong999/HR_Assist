namespace HR_Assist.Core.Services.Projects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using HR_Assist.Core.Constants;
    using HR_Assist.Core.Entities.Contexts;
    using HR_Assist.Core.Services.Common.Models;

    public class ProjectGetByIdHandler : IRequestHandler<ProjectGetByIdRequest, ResponseModel>
    {
        private readonly HR_AssistDbContext _db;
        private readonly IMapper _mapper;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectGetByIdHandler" /> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The auto mapper configuration.</param>
        public ProjectGetByIdHandler(
            HR_AssistDbContext db,
            IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(ProjectGetByIdRequest request, CancellationToken cancellationToken)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (project == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = HR_AssistMessageConstants.PROJECT_NOT_FOUND
                };
            }
            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = _mapper.Map<ProjectDTO>(project)
            };
        }
    }
}
