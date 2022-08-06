using Superkatten.Katministratie.Domain.Entities;
using System;
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

    public Location MapContractToDomain(ContractEntities.Location location)
    {
        var locationType = MapContractToDomain(location.Type);
        return new Location(location.Name, locationType)
        {
            Id = location.Id
        };
    }

    private static ContractEntities.LocationType MapDomainToContract(LocationType locationType)
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


    public LocationType MapContractToDomain(ContractEntities.LocationType locationType)
    {
        return locationType switch
        { 
            ContractEntities.LocationType.Farm => LocationType.Farm,
            ContractEntities.LocationType.PrivateProperty => LocationType.PrivateProperty,
            ContractEntities.LocationType.AllotmentGarden => LocationType.AllotmentGarden,
            ContractEntities.LocationType.Camping => LocationType.Camping,
            ContractEntities.LocationType.Farmhouse => LocationType.Farmhouse,
            ContractEntities.LocationType.Stable => LocationType.Stable,
            ContractEntities.LocationType.BusinessPark => LocationType.BusinessPark,
            ContractEntities.LocationType.UrbanArea => LocationType.UrbanArea,
            ContractEntities.LocationType.RuralArea => LocationType.RuralArea,
            ContractEntities.LocationType.NatureReserve => LocationType.NatureReserve,
            _ => throw new InvalidEnumArgumentException(nameof(locationType), (int)locationType, typeof(ContractEntities.LocationType))
        };
    }
}
