using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Shared.Request;

namespace PortalGalaxy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.FindByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoriaDtoRequest request)
        {
            return Ok(await _service.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, CategoriaDtoRequest request)
        {
            return Ok(await _service.UpdateAsync(id, request));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
