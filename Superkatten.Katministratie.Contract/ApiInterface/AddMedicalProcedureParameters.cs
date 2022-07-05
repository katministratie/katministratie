namespace Superkatten.Katministratie.Contract.ApiInterface;

public class AddMedicalProcedureParameters
{
    public Guid SuperkatId { get; init; }
    public int ProcedureType { get; init; }
    public DateTime Timestamp { get; init; }
    public string Remark { get; init; } = string.Empty;
}
