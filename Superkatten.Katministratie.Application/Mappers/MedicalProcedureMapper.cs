using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;
using System;
using System.ComponentModel;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers;

public class MedicalProcedureMapper : IMedicalProcedureMapper
{
    public ContractEntities.MedicalProcedure MapToContract(MedicalProcedure medicalProcedure)
    {
        return new ContractEntities.MedicalProcedure
        {
            Id = medicalProcedure.Id,
            ProcedureType = MapMedicalProcedureTypeToContract(medicalProcedure.ProcedureType),
            Remark = medicalProcedure.Remark,
            SuperkatId = medicalProcedure.SuperkatId,
            Timestamp = medicalProcedure.Timestamp
        };
    }

    private static int MapMedicalProcedureTypeToContract(MedicalProcedureType procedureType)
    {
        if (!Enum.TryParse<MedicalProcedureType>(procedureType.ToString(), true, out procedureType))
        {
            throw new InvalidEnumArgumentException(nameof(procedureType), (int)procedureType, typeof(MedicalProcedureType));
        }

        return (int)procedureType;
    }

    public MedicalProcedure MapToDomain(AddMedicalProcedureParameters parameters)
    {
        return new MedicalProcedure(
            MapMedicalProcedureTypeToDomain(parameters.ProcedureType),
            parameters.SuperkatId,
            parameters.Timestamp,
            parameters.Remark);
    }

    private static MedicalProcedureType MapMedicalProcedureTypeToDomain(ContractEntities.MedicalProcedureType procedureType)
    {
        return procedureType switch
        {
            ContractEntities.MedicalProcedureType.Stronghold => MedicalProcedureType.Stronghold,
            _ => throw new InvalidEnumArgumentException(nameof(procedureType), (int)procedureType, typeof(ContractEntities.MedicalProcedureType))
        };
    }
}