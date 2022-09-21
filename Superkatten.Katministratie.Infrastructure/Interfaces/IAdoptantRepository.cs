using Superkatten.Katministratie.Domain.Entities.Adoption;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Interfaces;

public interface IAdoptantRepository
{
    Task<Adoptant> GetAdoptantByNameAsync(string name);
    Task CreateAdoptant(Adoptant adoptant);
}
