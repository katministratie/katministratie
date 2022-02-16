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
            var superkat_1 = new SuperkatDtoBuilder().WithId(1).WithNumber(1).WithName("Piet").Build();
            var superkat_2 = new SuperkatDtoBuilder().WithId(2).WithNumber(2).WithName("Jan").Build();
            modelBuilder.Entity<SuperkatDto>().HasData(superkat_1);
            modelBuilder.Entity<SuperkatDto>().HasData(superkat_2);
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDtoBuilder().WithId(3).WithNumber(3).WithName("Klaas").Build());
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDtoBuilder().WithId(4).WithNumber(4).WithName("Trudy").Build());
            modelBuilder.Entity<SuperkatDto>().HasData(new SuperkatDtoBuilder().WithId(5).WithNumber(5).WithName("Johan").Build());

            var gastgezin = new GastgezinDtoBuilder().WithId(1)
                                         .WithName("Fam de Kroon")
                                         .WithAddress("Hooge Veld 1")
                                         .WithPhone("0625466984")
                                         .WithCity("Rhenoy")
                                         //.WithSuperkat(superkat_1)
                                         //.WithSuperkat(superkat_2)
                                         .Build();

            modelBuilder.Entity<GastgezinDto>().HasData(gastgezin);

            base.OnModelCreating(modelBuilder);
        }
    }
}