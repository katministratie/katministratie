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

    /// <summary>
    /// Get all superkatten between from (including from date) and to date.
    /// </summary>
    /// <param name="from">From date (included)</param>
    /// <param name="to">To date (excluded)</param>
    /// <returns>List of superkatten between (included)from and to date</returns>
    public async Task<IReadOnlyCollection<Superkat>> GetSuperkattenBetweenFromToAsync(DateTime from, DateTime to)
    {
        return await _context.SuperKatten
            .AsNoTracking()
            .Include(o => o.CatchLocation)
            .Where(o => o.CatchDate >= from && o.CatchDate < to)
            .ToListAsync();
    }
}