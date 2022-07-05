namespace Superkatten.Katministratie.Contract.Entities;

public class MedicalProcedure
{
    public Guid Id { get; private set; }
    public Guid SuperkatId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string? Remark { get; private set; }
    public int ProcedureType { get; private set; }
}