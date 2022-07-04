namespace Superkatten.Katministratie.Contract.Entities;

public class MedicalProcedure
{
    public Guid Id { get; init; }
    public Guid SuperkatId { get; init; }
    public DateTime Timestamp { get; init; }
    public string? Remark { get; init; }
    public int ProcedureType { get; init; }
}
