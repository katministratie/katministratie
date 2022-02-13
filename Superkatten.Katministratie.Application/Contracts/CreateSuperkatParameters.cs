namespace Superkatten.Katministratie.Application.Contracts
{
    public class CreateSuperkatParameters
    {
        public string Kleur { get; init; } = string.Empty;
        public string CatchLocation { get; init; } = string.Empty;
        public bool HasStronghold { get; init; } = false;
        public int DaysOld { get; set; }
    }
}
