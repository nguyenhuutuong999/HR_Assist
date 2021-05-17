namespace HR.Assist.Core.Services.Projects
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using HR.Assist.Core.Constants;
    using HR.Assist.Core.Entities.Contexts;
    using HR.Assist.Core.Services.Common.Models;

    public class ProjectEditHandler : IRequestHandler<ProjectEditRequest, ResponseModel>
    {
        private readonly HRAssistDbContext _db;
        private readonly IMapper _mapper;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectEditHandler" /> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The auto mapper configuration.</param>
        public ProjectEditHandler(
            HRAssistDbContext db,
            IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(ProjectEditRequest request, CancellationToken cancellationToken)
        {
            var project = await _db.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (project == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = HRAssistMessageConstants.PROJECT_NOT_FOUND
                };
            }

            _mapper.Map(request, project);

            await _db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = HRAssistMessageConstants.PROJECT_UPDATED_SUCCESSFULLY
            };
        }
    }
}
