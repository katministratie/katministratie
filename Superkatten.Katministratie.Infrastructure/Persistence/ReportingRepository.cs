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
            .Where(o => o.CatchDate >= from && o.CatchDate < to)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Superkat>> GetSuperkattenAtLocationAsync(CatArea catArea, int? cageNumber)
    {
        return await _context.SuperKatten
            .AsNoTracking()
            .Include(o => o.CatchLocation)
            .Where(o => o.CatArea == catArea && o.CageNumber == cageNumber)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Superkat>> GetSuperkattenAtAdopteesNotNeutralized()
    {
        var neutralizedSuperkatten = _context.MedicalProcedures
            .AsNoTracking()
            .Where(m => m.ProcedureType == MedicalProcedureType.Neutralize)
            .Select(m => m.SuperkatId)
            .ToList();

        var superkatten = await _context.SuperKatten
            .AsNoTracking()
            .Include(o => o.CatchLocation)
            .Where(o => o.GastgezinId != null && o.State != SuperkatState.Done && !neutralizedSuperkatten.Contains(o.Id))
            .ToListAsync();

        return Array.Empty<Superkat>();
    }

    public async Task<IReadOnlyCollection<Superkat>> GetSuperkattenAtRefugeNotNeutralized()
    {
        var neutralizedSuperkatten = _context.MedicalProcedures
            .AsNoTracking()
            .Where(m => m.ProcedureType == MedicalProcedureType.Neutralize)
            .Select(m => m.SuperkatId)
            .ToList();

        var superkatten = await _context.SuperKatten
            .AsNoTracking()
            .Include(o => o.CatchLocation)
            .Where(o => o.GastgezinId == null && o.State != SuperkatState.Done && !neutralizedSuperkatten.Contains(o.Id))
            .ToListAsync();

        return Array.Empty<Superkat>();
    }
}