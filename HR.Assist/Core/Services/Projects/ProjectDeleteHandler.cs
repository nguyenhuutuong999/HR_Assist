namespace HR.Assist.Core.Services.Projects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using HR.Assist.Core.Constants;
    using HR.Assist.Core.Entities.Contexts;
    using HR.Assist.Core.Services.Common.Models;

    public class ProjectDeleteHandler : IRequestHandler<ProjectDeleteRequest, ResponseModel>
    {
        private readonly HRAssistDbContext _db;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectDeleteHandler" /> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="mapper">The auto mapper configuration.</param>
        public ProjectDeleteHandler(HRAssistDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(ProjectDeleteRequest request, CancellationToken cancellationToken)
        {
            var Project = await _db.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (Project == null)
            {
                return new ResponseModel()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = HRAssistMessageConstants.PROJECT_DELETED_SUCCESSFULLY
                };
            }

            _db.Projects.Remove(Project);
            await _db.SaveChangesAsync(cancellationToken);

            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = HRAssistMessageConstants.PROJECT_DELETED_SUCCESSFULLY
            };
        }
    }
}
