using Superkatten.Katministratie.Domain.Entities;
using System.ComponentModel;
using ContractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Application.Mappers;

public class LocationMapper : ILocationMapper
{
    public ContractEntities.Location MapDomainToContract(Location location)
    {
        return new ContractEntities.Location
        {
            Id = location.Id,
            Name = location.Name,
            Type = MapDomainToContract(location.Type)
        };
    }

    private ContractEntities.LocationType MapDomainToContract(LocationType locationType)
    {
        return locationType switch
        {
            LocationType.Farm => ContractEntities.LocationType.Farm,
            LocationType.PrivateProperty => ContractEntities.LocationType.PrivateProperty,
            LocationType.AllotmentGarden => ContractEntities.LocationType.AllotmentGarden,
            LocationType.Camping => ContractEntities.LocationType.Camping,
            LocationType.Farmhouse => ContractEntities.LocationType.Farmhouse,
            LocationType.Stable => ContractEntities.LocationType.Stable,
            LocationType.BusinessPark => ContractEntities.LocationType.BusinessPark,
            LocationType.UrbanArea => ContractEntities.LocationType.UrbanArea,
            LocationType.RuralArea => ContractEntities.LocationType.RuralArea,
            LocationType.NatureReserve => ContractEntities.LocationType.NatureReserve,
            _ => throw new InvalidEnumArgumentException(nameof(locationType), (int)locationType, typeof(LocationType))
        };
    }
}
