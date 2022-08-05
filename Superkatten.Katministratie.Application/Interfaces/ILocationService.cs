using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces;

public interface ILocationService
{
    Task<IReadOnlyCollection<Location>> GetLocationsAsync();
}
