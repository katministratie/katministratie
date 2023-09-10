using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Persistance;

public class KatministratieContext : DbContext
{
    public KatministratieContext(DbContextOptions<KatministratieContext> options) 
        : base(options)
    {
        // EntityFramework needs this constructor
    }

    public DbSet<SuperkatDb> Superkatten => Set<SuperkatDb>();
    public DbSet<UserDb> Users => Set<UserDb>();
}
