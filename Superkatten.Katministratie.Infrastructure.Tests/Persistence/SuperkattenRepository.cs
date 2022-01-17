using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Domain.Interfaces;
using Superkatten.Katministratie.Infrastructure.Entities;
using Superkatten.Katministratie.Infrastructure.Exceptions;
using Superkatten.Katministratie.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Persistence
{
    public class SuperkattenRepository : ISuperkattenRepository
    {
        private readonly ILogger<SuperkattenRepository> _logger;
        private readonly SuperkattenDbContext _context;
        private readonly ISuperkatRepositoryMapper _mapper;
        public SuperkattenRepository(ILogger<SuperkattenRepository> logger, SuperkattenDbContext context, ISuperkatRepositoryMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Superkat> CreateSuperkatAsync(Superkat newSuperkat)
        {
            var superkatDto = _mapper.MapDomainToSuperkatDto(newSuperkat);    //TODO: is this the correct way

            await _context.SuperKatten.AddAsync(superkatDto);
            await _context.SaveChangesAsync();

            return newSuperkat;
        }

        public async Task DeleteSuperkatAsync(int superkatId)
        {
           var superkatDto = await _context
                .SuperKatten
                .Where(s => s.Id == superkatId)
                .FirstAsync();

            if (superkatDto == null)
            {
                throw new DatabaseException($"No superkat found in the database with id {superkatId}");
            }

            _context.SuperKatten.Remove(superkatDto);
            _context.SaveChanges();
        }

        public async Task<IReadOnlyCollection<Superkat>> GetAvailableSuperkattenAsync()
        {
            var superkatten = await _context
                .SuperKatten
                .Where(s => s.Number > 20102)
                .ToListAsync();

            return superkatten
                .Select(_mapper.MapSuperkatDtoToDomain)
                .AsQueryable()
                .ToListAsync();
        }


        public async Task<Superkat> GetSuperkatAsync(int superkatId)
        {
            var superkatDto = await _context
                .SuperKatten
                .Where(superkat => superkat.Number == superkatId)
                .FirstOrDefaultAsync();

            if (superkatDto == null)
            {
                throw new DatabaseException($"No superkat found in the database with id {superkatId}");
            }

            return _mapper.MapSuperkatDtoToDomain(superkatDto);
        }

        public async Task<Superkat> UpdateSuperkatAsync(Superkat updateSuperkatParameters)
        {
            var count = await _context
                .SuperKatten
                .Where(s => s.Number == updateSuperkatParameters.Number)
                .CountAsync();

            if (count <= 0)
            {
                throw new DatabaseException($"No superkat found in the database with Id {updateSuperkatParameters.Id} Number {updateSuperkatParameters.Number}");
            }

            var updatedSuperkat = superkat.CreateUpdatedModel(updateSuperkatParameters.Name);

            _context.Update(updatedSuperkatDto);
            await _context.SaveChangesAsync();
            
            return _mapper.MapSuperkatDtoToDomain(updatedSuperkat);
        }

        public int GetLastNumberForYear(int year)
        {
            var superkatCount = _context
                .SuperKatten?
                .Count(s => s.Number > year * 10000); //TODO: filter on year instead on calculation

            return superkatCount == null ? 0 : superkatCount.Value;
        }
    }
}
