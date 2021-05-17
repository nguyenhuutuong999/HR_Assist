using MediatR;
using HR.Assist.Core.Services.Common.Models;

namespace HR.Assist.Core.Services.Projects
{
    public class ProjectPageListRequest : BaseRequestModel, IRequest<ResponseModel>
    {
    }
}
