using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Domain.Entities.Adoption;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class SuperkattenDbContext : DbContext
{
    public DbSet<Adoptant> Adoptants => Set<Adoptant>();
    public DbSet<UserDto> Users => Set<UserDto>();
    public DbSet<Superkat> SuperKatten => Set<Superkat>();
    public DbSet<CatchOrigin> CatchOrigins => Set<CatchOrigin>();
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
