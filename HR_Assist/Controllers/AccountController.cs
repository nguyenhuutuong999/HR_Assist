using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading;
using System.Threading.Tasks;
using HR_Assist.Core.Services.Accounts;
using HR_Assist.Core.Infrastructure.Filters;

namespace HR_Assist.Controllers
{
    [Route("api/account")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        ///   Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<dynamic> Login([FromBody] UserLoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return new HR_AssistActionResult(result);
        }


    }
}
