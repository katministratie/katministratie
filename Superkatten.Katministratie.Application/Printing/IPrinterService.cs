using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Printing
{
    public interface IPrinterService
    {
        Task PrintPdfAsync(string filename, string printerName);
    }
}
