using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Shared.Request;
using QuestPDF.Fluent;

namespace PortalGalaxy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalleresController : ControllerBase
    {
        private readonly ITallerService _service;
        private readonly IPdfService _pdfService;
        private readonly IFileUploader _fileUploader;
        private readonly ILogger<TalleresController> _logger;

        public TalleresController(ITallerService service, IPdfService pdfService, 
            IFileUploader fileUploader,
            ILogger<TalleresController> logger)
        {
            _service = service;
            _pdfService = pdfService;
            _fileUploader = fileUploader;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BusquedaTallerRequest request)
        {
            var response = await _service.ListAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("inscritos")]
        public async Task<IActionResult> Get([FromQuery] BusquedaInscritosPorTallerRequest request)
        {
            var response = await _service.ListAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("simple")]
        public async Task<IActionResult> Get()
        {
            var response = await _service.ListSimpleAsync();

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.FindByIdAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TallerDtoRequest request)
        {
            var response = await _service.AddAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, TallerDtoRequest request)
        {
            var response = await _service.UpdateAsync(id, request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("pdf")]
        public async Task<IActionResult> Pdf(BusquedaTallerRequest request)
        {
            var response = await _pdfService.Generar(request);
            if (response.Success)
            {
                var bytes = response.Data.GeneratePdf();

                var url = await _fileUploader.UploadFileAsync(Convert.ToBase64String(bytes),
                    $"talleres-{Guid.NewGuid()}.pdf");

                _logger.LogInformation("Se subio el archivo de PDF en Azure {url}", url);

                return File(new MemoryStream(bytes), "application/pdf");
            }

            return Ok(response);
        }
    }
}
