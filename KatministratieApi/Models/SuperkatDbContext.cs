using Microsoft.EntityFrameworkCore;

public class SuperkatDbContext : DbContext
{
    public DbSet<SuperkatDto> Superkatten { get; set; }

    public SuperkatDbContext(DbContextOptions<SuperkatDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
