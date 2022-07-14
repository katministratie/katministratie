using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;

using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers;

public interface IMedicalProcedureMapper
{
    MedicalProcedureInformation MapToContract(string superkatNumber, MedicalProcedure medicalProcedure);
    MedicalProcedure MapToDomain(AddMedicalProcedureParameters parameters);
}
