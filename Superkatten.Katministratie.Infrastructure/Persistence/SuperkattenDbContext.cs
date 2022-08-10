using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class SuperkattenDbContext : DbContext
{
    public DbSet<UserDto> Users => Set<UserDto>();
    public DbSet<Superkat> SuperKatten => Set<Superkat>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Gastgezin> Gastgezinnen => Set<Gastgezin>();
    public DbSet<MedicalProcedure> MedicalProcedures => Set<MedicalProcedure>();

    public SuperkattenDbContext(DbContextOptions<SuperkattenDbContext> options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
