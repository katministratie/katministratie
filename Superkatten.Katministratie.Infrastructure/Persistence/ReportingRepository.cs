using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

internal class ReportingRepository : IReportingRepository
{
    private readonly SuperkattenDbContext _context;

    public ReportingRepository(SuperkattenDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Superkat>> GetSuperkattenBetweenFromToAsync(DateTime from, DateTime to)
    {
        return await _context.SuperKatten
            .AsNoTracking()
            .Include(o => o.CatchLocation)
            .Where(o => o.CatchDate >= from && o.CatchDate < to && o.State != SuperkatState.Done)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Superkat>> GetSuperkattenAtLocationAsync(CatArea catArea, int? cageNumber)
    {
        return await _context.SuperKatten
            .AsNoTracking()
            .Include(o => o.CatchLocation)
            .Where(o => o.CatArea == catArea && o.CageNumber == cageNumber && o.State != SuperkatState.Done)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Superkat>> GetNotNeutralizedSuperkatten()
    {
        var neutralizedSuperkatten = _context.MedicalProcedures
            .AsNoTracking()
            .Where(m => m.ProcedureType == MedicalProcedureType.Neutralize)
            .Select(m => m.SuperkatId)
            .ToList();

        return await _context.SuperKatten
            .AsNoTracking()
            .Include(o => o.CatchLocation)
            .Where(o => !neutralizedSuperkatten.Contains(o.Id) && o.State != SuperkatState.Done)
            .ToListAsync();
    }
}