using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Contract.ApiInterface;

public class MedicalProcedureInformation
{
    public string? SuperkatNumber { get; init; }
    public DateTime Timestamp { get; init; }
    public string? Remark { get; init; }
    public MedicalProcedureType ProcedureType { get; init; }
}
