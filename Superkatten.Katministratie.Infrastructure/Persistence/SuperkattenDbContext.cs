using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class SuperkattenDbContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<Superkat> SuperKatten { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Gastgezin> Gastgezinnen { get; set; }
    public DbSet<MedicalProcedure> MedicalProcedures { get; set; }

    public SuperkattenDbContext(DbContextOptions<SuperkattenDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
