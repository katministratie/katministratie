namespace Superkatten.Katministratie.Application.Contracts
{
    public class UpdateSuperkatParameters
    {
        public int? Number { get; init; }
        public string Name { get; init; } = string.Empty;
        public int DaysOld { get; init; } = 1;
    }
}
