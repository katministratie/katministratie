using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class SuperkattenDbContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<SuperkatDto> SuperKatten { get; set; }
    public DbSet<GastgezinDto> Gastgezinnen { get; set; }

    public SuperkattenDbContext(DbContextOptions<SuperkattenDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
