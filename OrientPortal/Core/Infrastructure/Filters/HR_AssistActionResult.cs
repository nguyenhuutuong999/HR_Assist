using Microsoft.AspNetCore.Mvc;
using HR_Assist.Core.Services.Common.Models;
using System.Threading.Tasks;

namespace HR_Assist.Core.Infrastructure.Filters
{
    public class HR_AssistActionResult : IActionResult
    {
        private readonly ResponseModel _responseModel;

        public HR_AssistActionResult(ResponseModel responseModel)
        {
            _responseModel = responseModel;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult objectResult;
            switch (_responseModel.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    objectResult = new ObjectResult(_responseModel.Data != null ? _responseModel.Data : _responseModel.Message)
                    {
                        StatusCode = (int)_responseModel.StatusCode
                    };
                    break;
                case System.Net.HttpStatusCode.NotFound:
                    objectResult = new ObjectResult(_responseModel.Message)
                    {
                        StatusCode = (int)_responseModel.StatusCode
                    };
                    break;
                default:
                    objectResult = new ObjectResult(_responseModel.Message)
                    {
                        StatusCode = (int)_responseModel.StatusCode
                    };
                    break;
            }
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
