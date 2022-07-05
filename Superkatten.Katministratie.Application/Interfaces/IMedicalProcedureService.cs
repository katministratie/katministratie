using Superkatten.Katministratie.Contract.ApiInterface;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces;

public interface IMedicalProcedureService
{
    Task AddMedicalProcedureAsyn(AddMedicalProcedureParameters parameters);
}
