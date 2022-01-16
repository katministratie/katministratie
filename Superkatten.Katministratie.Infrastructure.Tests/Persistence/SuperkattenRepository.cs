using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Exceptions;

namespace Superkatten.Katministratie.Infrastructure.Persistence
{
    public class SuperkattenRepository : ISuperkattenRepository
    {
        private readonly ILogger<SuperkattenRepository> _logger;
        private readonly SuperkattenDbContext _context;
       
        public SuperkattenRepository(ILogger<SuperkattenRepository> logger, SuperkattenDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task<Superkat> CreateSuperkatAsync(Superkat superkat)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteSuperkatAsync(int superkatId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyCollection<Superkat>> GetAvailableSuperkattenAsync()
        {
            var superkatten = _context
                .SuperKatten
                .Where(s => s.Number > 2021000);

            return new Task<IReadOnlyCollection<Superkat>>(() => 
            { 
                return superkatten.ToArray(); 
            });
        }

        public Task<Superkat> GetSuperkatAsync(int superkatId)
        {
            var superkat = _context
                .SuperKatten
                .FirstOrDefault(superkat => superkat.Number == superkatId);

            if (superkat == null)
            {
                throw new DatabaseException($"No superkat found in the database with id {superkatId}");
            }

            return Task.FromResult(superkat);  //TODO: get async call in this function
        }

        public Task<Superkat> UpdateSuperkatAsync(Superkat superkat)
        {
            throw new System.NotImplementedException();
        }

        public int GetLastNumberForYear(int year)
        {
            var superkatCount = _context.SuperKatten?.Count(); //TODO: filter on year

            return superkatCount == null ? 0 : superkatCount.Value;
        }
    }
}
