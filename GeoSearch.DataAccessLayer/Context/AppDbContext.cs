using GeoSearch.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoSearch.DataAccessLayer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<AppUser> Users { get; set; }
    public DbSet<GeoLocation> GeoLocations { get; set; }
    public DbSet<GeoLocationSearch> GeoLocationSearches { get; set; }
    public DbSet<FavouriteLocation> FavouriteLocations { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavouriteLocation>()
            .HasOne(uf => uf.User)
            .WithMany(u => u.FavouriteLocations)
            .HasForeignKey(uf => uf.UserId);

        modelBuilder.Entity<FavouriteLocation>()
            .HasOne(uf => uf.Location)
            .WithMany()
            .HasForeignKey(uf => uf.LocationId);
        
        modelBuilder.Entity<LocationCategory>()
            .HasKey(glc => new { glc.GeoLocationId, glc.CategoryId });

        modelBuilder.Entity<LocationCategory>()
            .HasOne(glc => glc.GeoLocation)
            .WithMany(g => g.LocationCategories)
            .HasForeignKey(glc => glc.GeoLocationId);

        modelBuilder.Entity<LocationCategory>()
            .HasOne(glc => glc.Category)
            .WithMany(c => c.LocationCategories)
            .HasForeignKey(glc => glc.CategoryId);
        
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
        
        modelBuilder.Entity<GeoLocation>()
            .HasMany(gl => gl.GeoLocationSearches)
            .WithMany(gls => gls.GeoLocations)
            .UsingEntity(j => j.ToTable("GeoLocationGeoLocationSearch"));

        base.OnModelCreating(modelBuilder);
    }
}