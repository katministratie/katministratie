using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.PdfGenerator
{
    public interface ISuperkatCardPdfGenerator
    {
        Task CreateSuperkatCardAsync(Guid id);
    }
}
