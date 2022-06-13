using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Exceptions;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using Superkatten.Katministratie.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class SuperkattenRepository : ISuperkattenRepository
{
    private readonly ILogger<SuperkattenRepository> _logger;
    private readonly SuperkattenDbContext _context;
    private readonly ISuperkatRepositoryMapper _mapper;
    public SuperkattenRepository(
        ILogger<SuperkattenRepository> logger, 
        SuperkattenDbContext context, 
        ISuperkatRepositoryMapper mapper
    )
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Superkat> CreateSuperkatAsync(Superkat superkat)
    {
        var isAvailable = await _context
            .SuperKatten
            .AnyAsync(s => s.Id == superkat.Id);
            
        if (isAvailable)
        {
            throw new DatabaseException($"A {nameof(Superkat)} found in the database with id {superkat.Id}");
        }
        
        var superkatDto = _mapper.MapDomainToRepository(superkat);

        await _context.SuperKatten.AddAsync(superkatDto);
        await _context.SaveChangesAsync();

        return await GetSuperkatAsync(superkatDto.Id);
    }

    public async Task DeleteSuperkatAsync(Guid id)
    {
       var superkatDto = await _context
            .SuperKatten
            .Where(s => s.Id == id)
            .FirstAsync();

        if (superkatDto is null)
        {
            throw new DatabaseException($"No superkat found in the database with id {id}");
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
            .Select(_mapper.MapRepositoryToDomain)
            .ToList();
    }

    public async Task<IReadOnlyCollection<Superkat>> GetNotAssignedSuperkattenAsync()
    {
        var superkatten = await _context
            .SuperKatten
            .Where(x => !_context
                        .Gastgezinnen
                        .Any(g => g.Superkatten.Contains(x))
            )
            .ToListAsync();

        return superkatten
            .Select(_mapper.MapRepositoryToDomain)
            .ToList();
    }



    public async Task<Superkat> GetSuperkatAsync(Guid id)
    {
        var superkatDto = await _context
            .SuperKatten
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();

        if (superkatDto is null)
        {
            throw new DatabaseException($"No superkat found in the database with id {id}");
        }

        return _mapper.MapRepositoryToDomain(superkatDto);
    }

    public async Task UpdateSuperkatAsync(Superkat superkat)
    {
        var isValidSuperkat = _context
            .SuperKatten
            .Any(s => s.Id == superkat.Id);

        if (!isValidSuperkat)
        {
            throw new DatabaseException($"No superkat found in the database with id {superkat.Id}");
        }

        var superkatDto = _mapper.MapDomainToRepository(superkat);

        _context.Update(superkatDto);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetSuperkatMaxNumberForGivenYearAsync(int year)
    {
        return await _context
            .SuperKatten
            .CountAsync(s => s.CatchDate.Year == year);
    }
}
