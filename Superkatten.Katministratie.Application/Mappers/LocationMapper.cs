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
        var catchOriginType = MapContractToDomain(location.Type);
        return new Location(location.Name, catchOriginType)
        {
            Id = location.Id
        };
    }

    private static ContractEntities.CatchOriginType MapDomainToContract(CatchOriginType catchOriginType)
    {
        return catchOriginType switch
        {
            CatchOriginType.Farm => ContractEntities.CatchOriginType.Farm,
            CatchOriginType.PrivateProperty => ContractEntities.CatchOriginType.PrivateProperty,
            CatchOriginType.AllotmentGarden => ContractEntities.CatchOriginType.AllotmentGarden,
            CatchOriginType.Camping => ContractEntities.CatchOriginType.Camping,
            CatchOriginType.Farmhouse => ContractEntities.CatchOriginType.Farmhouse,
            CatchOriginType.Stable => ContractEntities.CatchOriginType.Stable,
            CatchOriginType.BusinessPark => ContractEntities.CatchOriginType.BusinessPark,
            CatchOriginType.UrbanArea => ContractEntities.CatchOriginType.UrbanArea,
            CatchOriginType.RuralArea => ContractEntities.CatchOriginType.RuralArea,
            CatchOriginType.NatureReserve => ContractEntities.CatchOriginType.NatureReserve,
            _ => throw new InvalidEnumArgumentException(nameof(catchOriginType), (int)catchOriginType, typeof(CatchOriginType))
        };
    }


    public CatchOriginType MapContractToDomain(ContractEntities.CatchOriginType catchOriginType)
    {
        return catchOriginType switch
        { 
            ContractEntities.CatchOriginType.Farm => CatchOriginType.Farm,
            ContractEntities.CatchOriginType.PrivateProperty => CatchOriginType.PrivateProperty,
            ContractEntities.CatchOriginType.AllotmentGarden => CatchOriginType.AllotmentGarden,
            ContractEntities.CatchOriginType.Camping => CatchOriginType.Camping,
            ContractEntities.CatchOriginType.Farmhouse => CatchOriginType.Farmhouse,
            ContractEntities.CatchOriginType.Stable => CatchOriginType.Stable,
            ContractEntities.CatchOriginType.BusinessPark => CatchOriginType.BusinessPark,
            ContractEntities.CatchOriginType.UrbanArea => CatchOriginType.UrbanArea,
            ContractEntities.CatchOriginType.RuralArea => CatchOriginType.RuralArea,
            ContractEntities.CatchOriginType.NatureReserve => CatchOriginType.NatureReserve,
            _ => throw new InvalidEnumArgumentException(nameof(catchOriginType), (int)catchOriginType, typeof(ContractEntities.CatchOriginType))
        };
    }
}
