using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Contract.ApiInterface;

public class ReallocateSuperkatParameters
{
    public Guid? GastgezinId { get; set; }
    public CatArea CatArea { get; set; }
    public int CageNumber { get; set; }
}
