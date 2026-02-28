using Breweryinator.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Breweryinator.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Beer> Beers => Set<Beer>();
    public DbSet<Batch> Batches => Set<Batch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Style).HasMaxLength(100).IsRequired();
            entity.Property(e => e.AlcoholByVolume).HasPrecision(4, 2);
            entity.Property(e => e.InternationalBitternessUnits).HasPrecision(5, 1);
        });

        modelBuilder.Entity<Batch>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.BatchNumber).HasMaxLength(50).IsRequired();
            entity.Property(e => e.VolumeInLitres).HasPrecision(8, 2);
            entity.HasOne(e => e.Beer)
                  .WithMany(b => b.Batches)
                  .HasForeignKey(e => e.BeerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
