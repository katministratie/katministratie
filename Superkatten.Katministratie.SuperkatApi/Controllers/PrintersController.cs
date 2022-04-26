using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.Infrastructure.Entities;
using Superkatten.Katministratie.Infrastructure.Printing;

namespace Superkatten.Katministratie.SuperkatApi.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class PrintersController
{
    private readonly IPrinterRepository _printerRepository;

    public PrintersController(IPrinterRepository printerRepository)
    {
        _printerRepository = printerRepository;
    }

    [HttpGet]
    public Task<List<Printer>> GetPrinters()
    {
        return Task.FromResult(_printerRepository.GetPrinterList());
    }
}
