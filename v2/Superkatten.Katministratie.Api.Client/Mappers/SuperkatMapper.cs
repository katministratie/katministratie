using Superkatten.Katministratie.Api.Client.Entities;
using Superkatten.Katministratie.Application.Contracts.Entities;

namespace Superkatten.Katministratie.Api.Client.Mappers;

public class SuperkatMapper : ISuperkatMapper
{
    public SuperkatView MapToView(SuperkatDto superkatDto)
    {
        return new SuperkatView(superkatDto);
    }
}
