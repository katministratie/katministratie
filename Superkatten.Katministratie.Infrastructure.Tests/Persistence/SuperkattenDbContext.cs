using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Persistence
{
    public class SuperkattenDbContext : DbContext
    {
        public SuperkattenDbContext(DbContextOptions<SuperkattenDbContext> options) : base(options)
        {
        }

        public DbSet<SuperkatDto> SuperKatten { get; set; }
        public DbSet<GastgezinDto> Gastgezinnen { get; set; }

        public string DbPath { get; } = string.Empty;
    }
}