using Superkatten.Katministratie.Contract.ApiInterface;

namespace Superkatten.Katministratie.Host.Services;

public interface IMedicalProcedureService
{
    Task AddMedicalProcedureAsync(AddMedicalProcedureParameters parameters);
}
