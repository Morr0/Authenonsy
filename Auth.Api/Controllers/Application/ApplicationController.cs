using System.Threading.Tasks;
using Auth.Api.Controllers.Application.Requests;
using Auth.Api.Controllers.Application.Responses;
using Auth.Api.Services.ApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers.Application
{
    [ApiController]
    [Route("Application")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromQuery] string creatorId,
            [FromBody] ApplicationCreateRequest dto)
        {
            var application = await _applicationService.Create(creatorId, dto).ConfigureAwait(false);

            return Ok(new ApplicationCreateResponse
            {
                ClientId = application.ClientId
            });
        }

        [HttpGet("{applicationClientId}")]
        public async Task<IActionResult> GetApplication([FromRoute] string applicationClientId)
        {
            var application = await _applicationService.Get(applicationClientId).ConfigureAwait(false);
            if (application is null) return NotFound();

            return Ok(application);
        }
    }
}