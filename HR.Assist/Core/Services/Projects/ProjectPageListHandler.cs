using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HR.Assist.Core.Entities.Contexts;
using HR.Assist.Core.Infrastructure.Utilities;
using HR.Assist.Core.Services.Common.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Assist.Core.Services.Projects
{
    public class ProjectPageListHandler : IRequestHandler<ProjectPageListRequest, ResponseModel>
    {
        private readonly HRAssistDbContext _db;
        private readonly IMapper _mapper;

        public ProjectPageListHandler(IMapper mapper, HRAssistDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseModel> Handle(ProjectPageListRequest request, CancellationToken cancellationToken)
        {
            var list = await _db.Projects.Where(
               x => (string.IsNullOrEmpty(request.Query))
                    || (x.Name.Contains(request.Query)))
                .ProjectTo<ProjectDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var viewModelProperties = ReflectionUtilities.GetAllPropertyNamesOfType(typeof(ProjectDTO));
            var sortPropertyName = !string.IsNullOrEmpty(request.SortName) ? request.SortName.ToLower() : string.Empty;
            string matchedPropertyName = viewModelProperties.FirstOrDefault(x => x == sortPropertyName);

            if (string.IsNullOrEmpty(matchedPropertyName))
            {
                matchedPropertyName = "Name";
            }

            var type = typeof(ProjectDTO);
            var sortProperty = type.GetProperty(matchedPropertyName);

            list = request.IsDesc ? list.OrderByDescending(x => sortProperty.GetValue(x, null)).ToList() : list.OrderBy(x => sortProperty.GetValue(x, null)).ToList();

            var pageList = new PagedList<ProjectDTO>(list, request.Offset ?? CommonConstants.Config.DEFAULT_SKIP, request.Limit ?? CommonConstants.Config.DEFAULT_TAKE);
            return new ResponseModel()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = pageList
            };
        }
    }
}
