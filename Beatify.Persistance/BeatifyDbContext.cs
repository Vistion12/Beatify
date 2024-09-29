using Beatify.Application.interfaces;
using Beatify.Domain;
using Beatify.Persistance.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Beatify.Persistance;

public class BeatifyDbContext(DbContextOptions<BeatifyDbContext> options) : DbContext(options), IBeatifyDbContext 
{
    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
