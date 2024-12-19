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
    
    public async Task<GeoLocationSearch> AddGeoLocationSearchWithLocations(GeoLocationSearch searchRequest, List<GeoLocation> geoLocations)
    {
        var allCategories = geoLocations
            .SelectMany(gl => gl.LocationCategories)
            .Select(lc => lc.Category.Name)
            .Distinct()
            .ToList();

        var existingCategories = await _context.Categories
            .Where(c => allCategories.Contains(c.Name))
            .ToDictionaryAsync(c => c.Name);

        var newCategories = allCategories
            .Where(name => !existingCategories.ContainsKey(name))
            .Select(name => new Category { Name = name })
            .ToList();

        if (newCategories.Any())
        {
            await _context.Categories.AddRangeAsync(newCategories);
            await _context.SaveChangesAsync();

            foreach (var newCategory in newCategories)
            {
                existingCategories[newCategory.Name] = newCategory;
            }
        }

        var existingGeoLocations = await _context.GeoLocations
            .Select(gl => new
            {
                gl.Latitude,
                gl.Longitude,
                gl.Name,
                Entity = gl
            })
            .ToListAsync();

        var geoLocationDictionary = existingGeoLocations
            .GroupBy(gl => new { gl.Latitude, gl.Longitude, gl.Name })
            .ToDictionary(g => g.Key, g => g.First().Entity);

        var newGeoLocations = geoLocations
            .Where(gl => !geoLocationDictionary.ContainsKey(new { gl.Latitude, gl.Longitude, gl.Name }))
            .Select(gl => new GeoLocation
            {
                Latitude = gl.Latitude,
                Longitude = gl.Longitude,
                Name = gl.Name,
            })
            .ToList();

        foreach (var newGeoLocation in newGeoLocations)
        {
            geoLocationDictionary[new { newGeoLocation.Latitude, newGeoLocation.Longitude, newGeoLocation.Name }] = newGeoLocation;
        }

        if (newGeoLocations.Any())
        {
            await _context.GeoLocations.AddRangeAsync(newGeoLocations);
            await _context.SaveChangesAsync();
        }

        searchRequest.GeoLocations = geoLocations
            .Select(gl => geoLocationDictionary[new { gl.Latitude, gl.Longitude, gl.Name }])
            .ToList();

        await _context.GeoLocationSearches.AddAsync(searchRequest);
        await _context.SaveChangesAsync();

        return searchRequest;
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
        return await _context.GeoLocationSearches
            .Include(search => search.GeoLocations)  
            .ToListAsync();
    }

    public async Task AddFavouriteLocation(int userId, int locationId)
    {
        var alreadyFavourite = await _context.FavouriteLocations
            .AnyAsync(f => f.UserId == userId && f.LocationId == locationId);

        if (alreadyFavourite)
        {
            throw new InvalidOperationException("This location is already marked as a favorite for the user.");
        }

        var favouriteLocation = new FavouriteLocation
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