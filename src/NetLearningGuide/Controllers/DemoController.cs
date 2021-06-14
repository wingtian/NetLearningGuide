using Mediator.Net;
using Microsoft.AspNetCore.Mvc;
using NetLearningGuide.Message.Basic;
using NetLearningGuide.Message.Commands.Demo;
using System.Threading.Tasks;

namespace NetLearningGuide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DemoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("AutofacInstancePerDependency"), HttpGet]
        public async Task<CommonResponse<string>> AutofacInstancePerDependency([FromQuery] DemoAutofacInstancePerDependencyCommand command)
        {
            var response = await _mediator.SendAsync<DemoAutofacInstancePerDependencyCommand, CommonResponse<string>>(command);
            return response;
        }

        [Route("AutofacInstancePerLifetime"), HttpGet]
        public async Task<CommonResponse<string>> AutofacInstancePerLifetime([FromQuery] DemoAutofacInstancePerLifetimeCommand command)
        {
            var response = await _mediator.SendAsync<DemoAutofacInstancePerLifetimeCommand, CommonResponse<string>>(command);
            return response;
        }
        [Route("AutofacSingleton"), HttpGet]
        public async Task<CommonResponse<string>> AutofacSingleton([FromQuery] DemoAutofacSingletonCommand command)
        {
            var response = await _mediator.SendAsync<DemoAutofacSingletonCommand, CommonResponse<string>>(command);
            return response;
        }
    }
}
