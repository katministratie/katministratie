using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.CageCard
{
    public interface ISuperkatCageCard
    {
        Task CreateCageCardAsync(Guid id);
    }
}
