using Superkatten.Katministratie.Contract.ApiInterface;
using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces;

public interface ISuperkatAction
{
    Task ToggleRetourAsync(Guid id );
    Task ToggleReserveAsync(Guid id);
    Task CreateSuperkatCageCardAsync(SuperkatCageCardPrintParameters parameters);
    Task AddMedicalProcedureAsync(AddMedicalProcedureParameters parameters);
}
