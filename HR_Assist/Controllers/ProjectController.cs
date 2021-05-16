using HR_Assist.Core.Entities;
using HR_Assist.Core.Entities.Contexts;
using HR_Assist.Core.Infrastructure.Filters;
using HR_Assist.Core.Services.Projects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HR_Assist.Controllers
{
    [Route("api/projects")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(Policy = "AdminAccess")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<dynamic> GetAsync([FromQuery] ProjectPageListRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return new HR_AssistActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<dynamic> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new ProjectGetByIdRequest { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            return new HR_AssistActionResult(result);
        }

        [HttpPost]
        public async Task<dynamic> PostAsync([FromBody] ProjectCreateRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return new HR_AssistActionResult(result);
        }

        [HttpPut("{id}")]
        public async Task<dynamic> PutAsync(Guid id, [FromBody] ProjectEditRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var result = await _mediator.Send(request, cancellationToken);
            return new HR_AssistActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<dynamic> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new ProjectDeleteRequest()
            {
                Id = id
            };
            var result = await _mediator.Send(request, cancellationToken);

            return new HR_AssistActionResult(result);

        }
    }
}
