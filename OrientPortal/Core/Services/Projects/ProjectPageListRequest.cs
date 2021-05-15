using MediatR;
using HR_Assist.Core.Services.Common.Models;

namespace HR_Assist.Core.Services.Projects
{
    public class ProjectPageListRequest : BaseRequestModel, IRequest<ResponseModel>
    {
    }
}
