using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Infrastructure.Persistence
{
    public class SuperkattenDbContext : DbContext
    {
        public SuperkattenDbContext(DbContextOptions<SuperkattenDbContext> options) : base(options)
        {
        }

        public DbSet<Superkat> SuperKatten { get; set; }
        public DbSet<Gastgezin> Gastgezinnen { get; set; }

        public string DbPath { get; } = string.Empty;
    }
}