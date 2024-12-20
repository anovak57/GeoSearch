using GeoSearch.DataAccessLayer.Context;
using GeoSearch.DataAccessLayer.Entities;
using GeoSearch.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace GeoSearch.DataAccessLayer.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly AppDbContext _context;

    public LocationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, Category>> GetExistingCategoriesAsync(IEnumerable<string> categoryNames)
    {
        return await _context.Categories
            .Where(c => categoryNames.Contains(c.Name))
            .ToDictionaryAsync(c => c.Name);
    }

    public async Task AddCategoriesAsync(IEnumerable<Category> newCategories)
    {
        await _context.Categories.AddRangeAsync(newCategories);
    }

    public async Task<List<GeoLocation>> GetExistingGeoLocationsAsync()
    {
        return await _context.GeoLocations.ToListAsync();
    }

    public async Task AddGeoLocationsAsync(IEnumerable<GeoLocation> newGeoLocations)
    {
        await _context.GeoLocations.AddRangeAsync(newGeoLocations);
    }

    public async Task AddGeoLocationSearchAsync(GeoLocationSearch geoLocationSearch)
    {
        await _context.GeoLocationSearches.AddAsync(geoLocationSearch);
    }

    public async Task<IEnumerable<GeoLocation>> GetGeoLocationsAsync()
    {
        return await _context.GeoLocations.ToListAsync();
    }

    public async Task<IEnumerable<GeoLocation>> GetGeoLocationsByCategoryAsync(string query)
    {
        return await _context.GeoLocations
            .Include(gl => gl.LocationCategories)
            .ThenInclude(lc => lc.Category)
            .Where(gl => gl.LocationCategories.Any(lc => lc.Category.Name.ToLower() == query.ToLower()))
            .ToListAsync();
    }

    public async Task<IEnumerable<GeoLocationSearch>> GetLocationSearchesAsync()
    {
        return await _context.GeoLocationSearches
            .Include(search => search.GeoLocations)
            .ToListAsync();
    }

    public async Task AddFavouriteLocationAsync(FavouriteLocation favouriteLocation)
    {
        await _context.FavouriteLocations.AddAsync(favouriteLocation);
    }

    public async Task<bool> IsFavouriteLocationAsync(int userId, int locationId)
    {
        return await _context.FavouriteLocations
            .AnyAsync(f => f.UserId == userId && f.LocationId == locationId);
    }

    public async Task<IEnumerable<GeoLocation>> GetFavouriteLocationsAsync(int userId)
    {
        return await _context.FavouriteLocations
            .Where(f => f.UserId == userId)
            .Select(f => f.Location)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
