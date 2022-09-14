using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Exceptions;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class AdoptantRepository : IAdoptantRepository
{
    private readonly SuperkattenDbContext _context;

    public AdoptantRepository(SuperkattenDbContext context)
    {
        _context = context;
    }

    public async Task CreateAdoptant(Adoptant adoptant)
    {
        var adoptantExsist = await _context
            .Adoptants
            .AnyAsync(a => a.Name == adoptant.Name);

        if (adoptantExsist)
        {
            throw new DatabaseException($"A {nameof(Adoptant)} found in the database with name {adoptant.Name}");
        }

        await _context.Adoptants.AddAsync(adoptant);
        await _context.SaveChangesAsync();
    }

    public async Task<Adoptant> GetAdoptantByNameAsync(string name)
    {
        var adoptant = await _context
            .Adoptants
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Name == name);

        return adoptant is null
            ? throw new DatabaseException($"No adoptant found in the database with name '{name}'")
            : adoptant;
    }
}
