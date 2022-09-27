using System.Collections.Generic;
using System.ComponentModel;

namespace Superkatten.Katministratie.Domain.Entities.Locations;

public class Refuge : LocationBase
{
    public override LocationType LocationType => LocationType.Refuge;

    public string Address { get; } = "Huigenstraat 49";
    public string Postcode { get; } = "4151 CC";
    public string City { get; } = "Acquoy";

    private CatArea CatArea {get; init;} = CatArea.Quarantine;
    private int? CageNumber { get; init; }


    public Refuge(CatArea catArea, int? cageNumber)
    {
        CatArea = catArea;
        CageNumber = cageNumber;
    }

    public static List<int> GetCageNumbersForCatArea(CatArea catArea)
    {
        return catArea switch
        {
            CatArea.Quarantine => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
            CatArea.Room2 => new List<int> { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 },
            CatArea.Infirmary => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
            CatArea.SmallEnclosure => new List<int> { 1, 2, 3, 4 },
            CatArea.BigEnclosure => new() { 1 },
            CatArea.HostFamily => new() { 1 },
            _ => throw new InvalidEnumArgumentException(nameof(catArea), (int)catArea, typeof(CatArea))
        };
    }
}
