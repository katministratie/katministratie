using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Contract.ApiInterface;

public class CreateSuperkatParameters
{
    public string CatchLocation { get; init; } = string.Empty;
    public int EstimatedWeeksOld { get; init; }
    public DateTime CatchDate { get; init; }
    public bool Retour { get; init; }
    public int? CageNumber { get; init; }
    public CatArea CatArea { get; init; }
    public CatBehaviour Behaviour { get; init; }
    public bool IsKitten { get; init; }
    public Gender Gender { get; init; }
    public LitterGranuleType LitterType { get; init; }
    public bool WetFoodAllowed { get; init; }
    public FoodType FoodType { get; init; }
    public string CatColor { get; set; } = string.Empty;
}
