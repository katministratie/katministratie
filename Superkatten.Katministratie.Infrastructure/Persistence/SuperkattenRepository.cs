using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Domain.Interfaces;
using Superkatten.Katministratie.Infrastructure.Exceptions;
using Superkatten.Katministratie.Infrastructure.Mapper;
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
            _mapper = mapper;
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<Superkat> CreateSuperkatAsync(Superkat superkat)
        {
            var existingSuperkatCount = await _context
                .SuperKatten
                .Where(s => s.Number == superkat.Number)
                .CountAsync();

            if (existingSuperkatCount > 0)
            {
                throw new DatabaseException($"A {nameof(Superkat)} found in the database with number {superkat.Number}");
            }
            
            var superkatDto = _mapper.MapDomainToSuperkatDto(superkat);

            await _context.SuperKatten.AddAsync(superkatDto);
            await _context.SaveChangesAsync();

            var addedSuperkat = await _context
                .SuperKatten
                .Where(s => s.Number == superkatDto.Number)
                .FirstOrDefaultAsync();

            if (addedSuperkat is null)
            {
                throw new DatabaseException($"Error adding superkat number '{superkat.Number}'");
            }

            return _mapper.MapSuperkatDtoToDomain(addedSuperkat);
        }

        public async Task DeleteSuperkatAsync(int superkatNumber)
        {
           var superkatDto = await _context
                .SuperKatten
                .Where(s => s.Number == superkatNumber)
                .FirstAsync();

            if (superkatDto == null)
            {
                throw new DatabaseException($"No superkat found in the database with number {superkatNumber}");
            }

            _context.SuperKatten.Remove(superkatDto);
            _context.SaveChanges();
        }

        public async Task<IReadOnlyCollection<Superkat>> GetAvailableSuperkattenAsync()
        {
            var superkatten = await _context
                .SuperKatten
                .ToListAsync();

            return superkatten
                .Select(_mapper.MapSuperkatDtoToDomain)
                .ToList();
        }


        public async Task<Superkat> GetSuperkatAsync(int superkatNumber)
        {
            var superkatDto = await _context
                .SuperKatten
                .Where(s => s.Number == superkatNumber)
                .FirstOrDefaultAsync();

            if (superkatDto == null)
            {
                throw new DatabaseException($"No superkat found in the database with number {superkatNumber}");
            }

            return _mapper.MapSuperkatDtoToDomain(superkatDto);
        }

        public async Task<Superkat> UpdateSuperkatAsync(Superkat superkat)
        {
            var superkatDto = await _context
                .SuperKatten
                .Where(s => s.Number == superkat.Number)
                .FirstAsync();

            if (superkatDto == null)
            {
                throw new DatabaseException($"No superkat found in the database with Number {superkat.Number}");
            }

            superkatDto.Name = superkat.Name;

            _context.Update(superkatDto);
            await _context.SaveChangesAsync();
            
            return _mapper.MapSuperkatDtoToDomain(superkatDto);
        }

        public async Task<int> GetSuperkatMaxNumberForGivenYearAsync(int year)
        {
            var superkatten = await _context
                .SuperKatten
                .Where(s => s.FoundDate.Year == year)
                .ToArrayAsync();

            if (superkatten.Length == 0)
            {
                return 0;
            }

            return superkatten.Max(s => s.Number);
        }
    }
}
