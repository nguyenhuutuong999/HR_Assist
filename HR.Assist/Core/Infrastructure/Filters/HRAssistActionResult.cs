using Microsoft.AspNetCore.Mvc;
using HR.Assist.Core.Services.Common.Models;
using System.Threading.Tasks;

namespace HR.Assist.Core.Infrastructure.Filters
{
    public class HRAssistActionResult : IActionResult
    {
        private readonly ResponseModel _responseModel;

        public HRAssistActionResult(ResponseModel responseModel)
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
