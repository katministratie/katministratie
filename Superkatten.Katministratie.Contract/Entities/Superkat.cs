namespace Superkatten.Katministratie.Contract.Entities;

public class Superkat
{
    public Guid Id { get; init; }
    public int Number { get; init; }
    public DateTime Birthday { get; init; }
    public DateTime CatchDate { get; init; }
    public string CatchLocation { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public bool Reserved { get; init; }
    public bool Retour { get; init; }
    public CatArea CatArea { get; init; }
    public int? CageNumber { get; init; }
    public CatBehaviour Behaviour { get; init; }
    public bool IsKitten { get; init; } = true;
    public Gender Gender { get; set; }
}
