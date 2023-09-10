using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Domain;
using Superkatten.Katministratie.Infrastructure.Entities;
using Superkatten.Katministratie.Infrastructure.Exceptions;
using Superkatten.Katministratie.Infrastructure.Mappers;
using Superkatten.Katministratie.Infrastructure.Persistance;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Superkatten.Katministratie.Infrastructure.Services;

public class SuperkattenRepository : ISuperkattenRepository
{
    private KatministratieContext _context;
    private readonly ISuperkatMapper _mapper;

    public SuperkattenRepository(
        KatministratieContext context,
        ISuperkatMapper mapper
    )
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<int> GetMaxSuperkatNumberForYear(int year)
    {
        if (_context.Superkatten.Count().Equals(0))
        {
            return 0;
        }

        return await _context
            .Superkatten
            .MaxAsync(x => x.Number);
    }

    public async Task<List<Superkat>> GetSuperkattenAsync()
    {
        var superkatten = await _context
            .Superkatten
            .ToListAsync();

        return superkatten
            .Select(_mapper.MapToDomain)
            .ToList();
    }

    public async Task CreateSuperkatAsync(Superkat superkat)
    {
        var superkatExsists = await _context
            .Superkatten
            .AnyAsync(x => x.Number == superkat.Number);
        if (superkatExsists)
        {
            throw new InfrastructureException($"Superkat number {superkat.Number} already exsists");
        }

        var superkatDb = _mapper.MapFromDomain(superkat);

        await _context.Superkatten.AddAsync(superkatDb);
        await _context.SaveChangesAsync();
    }

    public async Task<Superkat> ReadSuperkatAsyc(int number)
    {
        await CheckIfSuperkatNumberExsist(number);

        var superkat = await _context
            .Superkatten
            .Where(x => x.Number == number)
            .FirstAsync();

        return _mapper.MapToDomain(superkat);
    }

    public async Task UpdateSuperkatAsync(Superkat superkat)
    {
        await CheckIfSuperkatNumberExsist(superkat.Number);
        
        var superkatDb = _mapper.MapFromDomain(superkat);
        
        superkatDb.Id = await _context
            .Superkatten
            .Where(x => x.Number == superkat.Number)
            .Select(x => x.Id)
            .FirstAsync();

        _context.Superkatten.Update(superkatDb);
        _context.SaveChanges();
    }

    public Task DeleteSuperkatAsync(int number)
    {
        // TODO: Willen we eigenlijk wel kattenkunnen verwijderen....  hoe loopt het danmet
        //       de unieke katnummers ?
        throw new NotImplementedException();
    }


    private async Task CheckIfSuperkatNumberExsist(int number)
    {
        var superkatExsists = await _context
            .Superkatten
            .AnyAsync(x => x.Number == number);

        if (!superkatExsists)
        {
            throw new InfrastructureException($"Superkat number {number} does not exsists");
        }
    }

}
