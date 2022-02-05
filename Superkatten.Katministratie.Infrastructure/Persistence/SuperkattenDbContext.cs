using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Infrastructure.Entities;
using System;

namespace Superkatten.Katministratie.Infrastructure.Persistence
{
    public class SuperkattenDbContext : DbContext
    {
        public DbSet<SuperkatDto>? SuperKatten { get; set; }
        public DbSet<GastgezinDto>? Gastgezinnen { get; set; }

        public SuperkattenDbContext(DbContextOptions<SuperkattenDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 1, Number = 1, Name = "Jan", FoundDate = System.DateTimeOffset.Now });
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 2, Number = 2, Name = "Piet", FoundDate = System.DateTimeOffset.Now });
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 3, Number = 3, Name = "Frances", FoundDate = System.DateTimeOffset.Now });
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 4, Number = 4, Name = "Caitlyn", FoundDate = System.DateTimeOffset.Now });
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDto { Id = 5, Number = 5, Name = "Trudy", FoundDate = System.DateTimeOffset.Now });

            modelBuilder.Entity<SuperkatDetailsDto>().HasData(new SuperkatDetailsDto { Id = 1, Entered = DateTime.Parse("2022-01-25 08:00:00"), Title = "Bezoek dierenarts", Description = "Checkup" });
            modelBuilder.Entity<SuperkatDetailsDto>().HasData(new SuperkatDetailsDto { Id = 2, Entered = DateTime.Parse("2022-01-27 08:00:00"), Title = "Bezoek dierenarts", Description = "Checkup" });
            modelBuilder.Entity<SuperkatDetailsDto>().HasData(new SuperkatDetailsDto { Id = 3, Entered = DateTime.Parse("2022-02-2 08:00:00"), Title = "Bezoek dierenarts", Description = "Checkup" });

            modelBuilder.Entity<GastgezinDto>().HasData(new GastgezinDto { Id = 1, Name = "Fam de Kroon", Address = "Hooge Veld 1", City = "Rhenoy", Phone = "0625466984" });
            modelBuilder.Entity<GastgezinDto>().HasData(new GastgezinDto { Id = 2, Name = "Fam Viscer", Address = "Hooge Veld 1", City = "Rhenoy", Phone = "onbekend" });

            base.OnModelCreating(modelBuilder);
        }
    }
}