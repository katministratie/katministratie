using System.Collections.Generic;
using System.ComponentModel;

namespace Superkatten.Katministratie.Domain.Entities;

public class Refuge
{
    public string Address { get; } = "Huigenstraat 49";
    public string Postcode { get; } = "4151 CC";
    public string City { get; } = "Acquoy";
    
    public static List<int> GetCageNumbersForCatArea(CatArea catArea)
    {
        return catArea switch
        {
            CatArea.Quarantine => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 },
            CatArea.Room2 => new List<int> { 19, 20, 21, 22, 23, 24, 25, 26 ,27, 28, 29, 30, 31, 32, 33 },
            CatArea.Infirmary => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
            CatArea.SmallEnclosure => new List<int> { 1, 2, 3, 4, 5 },
            CatArea.BigEnclosure => new() { 1 },
            _ => throw new InvalidEnumArgumentException(nameof(catArea), (int)catArea, typeof(CatArea))
        };
    }
}
