using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Infrastructure.Entities;
using Superkatten.Katministratie.Infrastructure.Printing;

namespace Superkatten.Katministratie.SuperkatApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EnvironmentController
    {
        private readonly IPrinterRepository _printerRepository;

        public EnvironmentController(IPrinterRepository printerRepository)
        {
            _printerRepository = printerRepository;
        }

        [HttpGet]
        [Route("Printers")]
        public async Task<IReadOnlyCollection<Printer>> GetAllPrinters()
        {
            var printers = _printerRepository.GetPrinterList();
            return printers;
        }
    }
}
