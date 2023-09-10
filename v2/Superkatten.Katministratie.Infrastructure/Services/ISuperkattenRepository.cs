using Superkatten.Katministratie.Domain;

namespace Superkatten.Katministratie.Infrastructure.Services;

public interface ISuperkattenRepository
{
    // CRUD interface
    Task CreateSuperkatAsync(Superkat superkat);
    Task<Superkat> ReadSuperkatAsyc(int number);
    Task UpdateSuperkatAsync(Superkat superkat);
    Task DeleteSuperkatAsync(int number);

    Task<List<Superkat>> GetSuperkattenAsync();
    Task<int> GetMaxSuperkatNumberForYear(int year);
}