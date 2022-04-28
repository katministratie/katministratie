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
    public class GastgezinnenRepository : IGastgezinnenRepository
    {
        private readonly ILogger<GastgezinnenRepository> _logger;
        private readonly SuperkattenDbContext _context;
        private readonly IGastgezinRepositoryMapper _mapper;
        public GastgezinnenRepository(ILogger<GastgezinnenRepository> logger, SuperkattenDbContext context, IGastgezinRepositoryMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _context.Database.Migrate();
        }

        public async Task<Gastgezin> CreateGastgezinAsync(Gastgezin createGastgezin)
        {
            var existingGastgezinCount = await _context
                .Gastgezinnen
                .CountAsync(gg => gg.Name == createGastgezin.Name);

            if (existingGastgezinCount > 0)
            {
                throw new DatabaseException($"A gastgezin found in the database with name '{createGastgezin.Name}'");
            }

            var gastgezinDto = _mapper.MapDomainToGastgezinDto(createGastgezin);

            await _context.Gastgezinnen.AddAsync(gastgezinDto);
            await _context.SaveChangesAsync();

            var addedGastgezin = await _context
                .Gastgezinnen
                .Where(gg => gg.Name == createGastgezin.Name)
                .FirstOrDefaultAsync();

            if (addedGastgezin is null)
            {
                throw new DatabaseException($"Error adding gastgezin '{createGastgezin.Name}'");
            }

            return _mapper.MapGastgezinDtoToDomain(addedGastgezin);
        }

        public async Task DeleteGastgezinAsync(string name)
        {
            var gastgezinDto = await _context
                .Gastgezinnen
                .Where(gg => gg.Name == name)
                .FirstAsync();

            if (gastgezinDto is null)
            {
                throw new DatabaseException($"No gastgezin found in the database with name '{name}'");
            }

            _context.Gastgezinnen.Remove(gastgezinDto);
            _context.SaveChanges();
        }

        public async Task<IReadOnlyCollection<Gastgezin>> GetGastgezinnenAsync()
        {
            var gastgezinnen = await _context
                .Gastgezinnen
                .ToListAsync();

            return gastgezinnen
                .Select(_mapper.MapGastgezinDtoToDomain)
                .ToList();
        }

        public async Task<Gastgezin> GetGastgezinAsync(string name)
        {
            var gastgezinDto = await _context
                .Gastgezinnen
                .Where(gg => gg.Name == name)
                .FirstOrDefaultAsync();

            if (gastgezinDto is null)
            {
                throw new DatabaseException($"No gastgezin found in the database with name '{name}'");
            }

            return _mapper.MapGastgezinDtoToDomain(gastgezinDto);
        }

        public async Task<Gastgezin> UpdateGastgezinAsync(string name, Gastgezin gastgezin)
        {
            var gastgezinDto = await _context
                .Gastgezinnen
                .Where(gg => gg.Name == name)
                .FirstAsync();

            if (gastgezinDto is null)
            {
                throw new DatabaseException($"No gastgezin found in the database with name '{name}'");
            }

            gastgezinDto.Name = gastgezin.Name;
            gastgezinDto.Address = gastgezin.Address;
            gastgezinDto.City = gastgezin.City;
            gastgezinDto.Phone = gastgezin.Phone;

            _context.Update(gastgezinDto);
            await _context.SaveChangesAsync();

            return _mapper.MapGastgezinDtoToDomain(gastgezinDto);
        }
    }
}
