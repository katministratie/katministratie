using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Persistence
{
    public class SuperkattenDbContext : DbContext
    {
        public SuperkattenDbContext(DbContextOptions<SuperkattenDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 1, Number = 1, Name = "Jan" });
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 2, Number = 2, Name = "Piet" });
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 3, Number = 3, Name = "Frances" });
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 4, Number = 4, Name = "Caitlyn" });
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 5, Number = 5, Name = "Trudy" });

            modelBuilder.Entity<GastgezinDto>().HasData(new GastgezinDto { Id = 1, Name = "Fam de Kroon", Address = "Hooge Veld 1", City = "Rhenoy", Phone = "0625466984" });
            modelBuilder.Entity<GastgezinDto>().HasData(new GastgezinDto { Id = 2, Name = "Fam Viscer", Address = "Hooge Veld 1", City = "Rhenoy", Phone = "onbekend" });
        }

        public DbSet<SuperkatDto> SuperKatten { get; set; }
        public DbSet<GastgezinDto> Gastgezinnen { get; set; }
    }
}