namespace Superkatten.Katministratie.Contract.Entities;

//TODO: Wellicht aanpassen naar LocationBase (type = t1, t2, t3) en types:
//      - t1: CatchLocation (type and name) 
//      - t2: RefugeLocation (area en cagenumber)
//      - t3: HostFamily (gastgezin guid)
public class Location
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public LocationType Type { get; init; }
}
