using Superkatten.Katministratie.Contract.ApiInterface;

namespace Superkatten.Katministratie.Host.Services.Interfaces;

public interface IMedicalProcedureService
{
    Task AddMedicalProcedure(Guid superkatId, AddMedicalProcedureParameters parameters);
}
