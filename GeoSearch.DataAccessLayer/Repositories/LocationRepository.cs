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

    public async Task<IEnumerable<GeoLocation>> AddGeoLocations(IEnumerable<GeoLocation> locations)
    {
        var locationList = locations.ToList();

        var uniqueCategoryNames = locationList
            .SelectMany(location => location.LocationCategories.Select(glc => glc.Category.Name))
            .Distinct()
            .ToHashSet();

        var existingCategories = await _context.Categories
            .Where(c => uniqueCategoryNames.Contains(c.Name))
            .ToDictionaryAsync(c => c.Name);

        var newCategories = uniqueCategoryNames
            .Where(name => !existingCategories.ContainsKey(name))
            .Select(name => new Category { Name = name })
            .ToList();

        _context.Categories.AddRange(newCategories);

        var categoryDict = existingCategories
            .Concat(newCategories.ToDictionary(c => c.Name))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        foreach (var location in locationList)
        {
            foreach (var geoLocationCategory in location.LocationCategories)
            {
                geoLocationCategory.Category = categoryDict[geoLocationCategory.Category.Name];
            }
        }

        await _context.SaveChangesAsync();
        await _context.GeoLocations.AddRangeAsync(locationList);
        await _context.SaveChangesAsync();

        return locationList;
    }

    public async Task<GeoLocationSearch> AddLocationSearch(GeoLocationSearch locationSearch)
    {
        await _context.GeoLocationSearches.AddAsync(locationSearch);
        await _context.SaveChangesAsync();
        
        return locationSearch;
    }

    public async Task<IEnumerable<GeoLocation>> GetGeoLocations()
    {
        return await _context.GeoLocations.ToListAsync();
    }

    public async Task<IEnumerable<GeoLocation>> GetGeoLocationsByCategory(string query)
    {
        return await _context.GeoLocations
            .Include(g => g.LocationCategories)
            .ThenInclude(lc => lc.Category)
            .Where(g => g.LocationCategories
                .Any(glc => glc.Category.Name.ToLower() == query.ToLower()))
            .ToListAsync();
    }

    public async Task<IEnumerable<GeoLocationSearch>> GetLocationSearches()
    {
        return await _context.GeoLocationSearches.ToListAsync();
    }

    public async Task AddFavouriteLocation(int userId, int locationId)
    {
        var favouriteLocation = new FavouriteLocation()
        {
            UserId = userId,
            LocationId = locationId
        };
        
        await _context.FavouriteLocations.AddAsync(favouriteLocation);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<GeoLocation>> GetFavouriteLocations(int userId)
    {
        var favouriteLocations = await _context.FavouriteLocations
            .Where(uf => uf.UserId == userId)
            .Select(uf => uf.Location)
            .ToListAsync();
        
        return favouriteLocations;
    }
}