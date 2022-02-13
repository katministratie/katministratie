using Microsoft.EntityFrameworkCore;
using Superkatten.Katministratie.Infrastructure.Builders;
using Superkatten.Katministratie.Infrastructure.Entities;
using System;

namespace Superkatten.Katministratie.Infrastructure.Persistence
{
    public class SuperkattenDbContext : DbContext
    {
        public DbSet<SuperkatDto> SuperKatten { get; set; }
        public DbSet<GastgezinDto> Gastgezinnen { get; set; }

        public SuperkattenDbContext(DbContextOptions<SuperkattenDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDtoBuilder().WithId(1).WithNumber(1).WithName("Piet").Build());
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDtoBuilder().WithId(2).WithNumber(2).WithName("Jan").Build());
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDtoBuilder().WithId(3).WithNumber(3).WithName("Klaas").Build());
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDtoBuilder().WithId(4).WithNumber(4).WithName("Trudy").Build());
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDtoBuilder().WithId(5).WithNumber(5).WithName("Johan").Build());

            modelBuilder.Entity<GastgezinDto>().HasData(new GastgezinDto { Id = 1, Name = "Fam de Kroon", Address = "Hooge Veld 1", City = "Rhenoy", Phone = "0625466984" });
            modelBuilder.Entity<GastgezinDto>().HasData(new GastgezinDto { Id = 2, Name = "Fam Viscer", Address = "Hooge Veld 1", City = "Rhenoy", Phone = "onbekend" });

            base.OnModelCreating(modelBuilder);
        }
    }
}