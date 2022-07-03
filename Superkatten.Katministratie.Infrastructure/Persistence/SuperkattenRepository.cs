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

public class SuperkattenRepository : ISuperkattenRepository
{
    private readonly ILogger<SuperkattenRepository> _logger;
    private readonly SuperkattenDbContext _context;
    public SuperkattenRepository(
        ILogger<SuperkattenRepository> logger, 
        SuperkattenDbContext context
    )
    {
        _logger = logger;
        _context = context;
    }

    public async Task CreateSuperkatAsync(Superkat superkat)
    {
        var superkatDtoExsist = await _context
            .SuperKatten
            .AnyAsync(s => s.Id == superkat.Id);            
        if (superkatDtoExsist)
        {
            throw new DatabaseException($"A {nameof(Superkat)} found in the database with id {superkat.Id}");
        }

        await _context.SuperKatten.AddAsync(superkat);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSuperkatAsync(Guid guid)
    {
       var superkat = await _context
            .SuperKatten
            .FirstOrDefaultAsync(s => s.Id == guid);
        if (superkat is null)
        {
            throw new DatabaseException($"No superkat found in the database with id {guid}");
        }

        _context.SuperKatten.Remove(superkat);
        _context.SaveChanges();
    }

    public async Task<IReadOnlyCollection<Superkat>> GetAssignedSuperkattenAsync()
    {
        return await _context
            .SuperKatten
            .AsNoTracking()
            .Where(o => o.GastgezinId != null)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Superkat>> GetNotAssignedSuperkattenAsync()
    {
        return await _context
            .SuperKatten
            .AsNoTracking()
            .Where(o => o.GastgezinId == null)
            .ToListAsync();
    }

    public async Task<Superkat> GetSuperkatAsync(Guid id)
    {
        var superkat = await _context
            .SuperKatten
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (superkat is null)
        {
            throw new DatabaseException($"No superkat found in the database with id {id}");
        }

        return superkat;
    }

    public async Task UpdateSuperkatAsync(Superkat superkat)
    {
        var superkatExist = await _context
            .SuperKatten
            .AnyAsync(s => s.Id == superkat.Id);

        if (!superkatExist)
        {
            throw new DatabaseException($"No superkat found in the database with id {superkat.Id}");
        }

        _context.Update(superkat);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetNextUniqueSuperkatNumber(int year)
    {
        var count = await _context
            .SuperKatten
            .CountAsync(s => s.CatchDate.Year == year);

        var maxNumber = await _context
            .SuperKatten
            .Where(s => s.CatchDate.Year == year)
            .MaxAsync(s => s.Number);

        return count is 0
            ? 1
            : await _context
                    .SuperKatten
                    .Where(s => s.CatchDate.Year == year)
                    .MaxAsync(s => s.Number) + 1;
    }
}
