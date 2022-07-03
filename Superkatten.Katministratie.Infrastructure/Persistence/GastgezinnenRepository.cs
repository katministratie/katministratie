using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Exceptions;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class GastgezinnenRepository : IGastgezinnenRepository
{
    private readonly ILogger<GastgezinnenRepository> _logger;
    private readonly SuperkattenDbContext _context;
    public GastgezinnenRepository(
        ILogger<GastgezinnenRepository> logger, 
        SuperkattenDbContext context
    )
    {
        _logger = logger;
        _context = context;
    }

    public async Task CreateGastgezinAsync(Gastgezin gastgezin)
    {
        var gastgezinExists = await _context
            .Gastgezinnen
            .AnyAsync(o => o.Name == gastgezin.Name);

        if (gastgezinExists)
        {
            throw new DatabaseException($"A hostfamily found in the database with name '{gastgezin.Name}'");
        }

        await _context.Gastgezinnen.AddAsync(gastgezin);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Hostfamily {Name} created", gastgezin.Name);
    }

    public async Task DeleteGastgezinAsync(Guid id)
    {
        var gastgezin = await _context
            .Gastgezinnen
            .AsNoTracking()
            .Where(o => o.Id == id)
            .FirstAsync();

        if (gastgezin is null)
        {
            throw new DatabaseException($"No hostfamily found in the database with id '{id}'");
        }

        _context.Gastgezinnen.Remove(gastgezin);
        _context.SaveChanges();

        _logger.LogInformation("Hostfamily {Name} created", gastgezin.Name);
    }

    public async Task<IReadOnlyCollection<Gastgezin>> GetGastgezinnenAsync()
    {
        return await _context
            .Gastgezinnen
            .ToListAsync();
    }

    public async Task<Gastgezin> GetGastgezinAsync(Guid id)
    {
        var gastgezin = await _context
            .Gastgezinnen
            .AsNoTracking()
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();

        return gastgezin is not null
            ? gastgezin
            : throw new DatabaseException($"Unknown hostfamily id '{id}'");
    }

    public async Task UpdateGastgezinAsync(Gastgezin gastgezin)
    {
        var gastgezinExsist = await _context
            .Gastgezinnen
            .AnyAsync(o => o.Id == gastgezin.Id);
        if (!gastgezinExsist)
        {
            throw new DatabaseException($"Hostfamily '{gastgezin.Name}' not found");
        }

        _context.Gastgezinnen.Update(gastgezin);
        _ = await _context.SaveChangesAsync();

        _logger.LogInformation("Hostfamily {Name} updated", gastgezin.Name);
    }
}
