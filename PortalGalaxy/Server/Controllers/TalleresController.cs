using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Shared.Request;

namespace PortalGalaxy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalleresController : ControllerBase
    {
        private readonly ITallerService _service;

        public TalleresController(ITallerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BusquedaTallerRequest request)
        {
            var response = await _service.ListAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
