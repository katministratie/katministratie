using Superkatten.Katministratie.Api.Client.Entities;
using Superkatten.Katministratie.Application.Contracts.Entities;

namespace Superkatten.Katministratie.Api.Client.Mappers;

public interface ISuperkatMapper
{
    SuperkatView MapToView(SuperkatDto superkatDto);
}