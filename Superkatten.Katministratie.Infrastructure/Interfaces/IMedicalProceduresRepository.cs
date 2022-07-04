using Superkatten.Katministratie.Contract.ApiInterface;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Interfaces;

public interface IMedicalProceduresRepository
{
    public Task AddMedicalProcedureAsync(AddMedicalProcedureParameters addMedicalProcedureParameters);
}
