using Microsoft.EntityFrameworkCore;
using SpatialGpt.Api.Models;

namespace SpatialGpt.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Property> Properties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>(entity =>
        {
            entity.ToTable("properties");
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Title).HasColumnName("title");
            entity.Property(p => p.Type).HasColumnName("type");
            entity.Property(p => p.Price).HasColumnName("price");
            entity.Property(p => p.City).HasColumnName("city");
            entity.Property(p => p.Area).HasColumnName("area");
            entity.Property(p => p.Furnished).HasColumnName("furnished");
            entity.Property(p => p.Geom).HasColumnName("geom");
        });
    }
}