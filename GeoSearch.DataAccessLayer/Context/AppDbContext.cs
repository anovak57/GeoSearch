using GeoSearch.DataAccessLayer.Data.Seeds;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}