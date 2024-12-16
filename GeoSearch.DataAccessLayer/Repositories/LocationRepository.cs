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
        
        _context.GeoLocations.AddRange(locationList);
        await _context.SaveChangesAsync();
        
        return locationList;
    }

    public async Task<GeoLocationSearch> AddLocationSearch(GeoLocationSearch locationSearch)
    {
        _context.GeoLocationSearches.Add(locationSearch);
        await _context.SaveChangesAsync();
        
        return locationSearch;
    }

    public async Task<IEnumerable<GeoLocation>> GetGeoLocations()
    {
        return await _context.GeoLocations.ToListAsync();
    }

    public async Task<IEnumerable<GeoLocationSearch>> GetLocationSearches()
    {
        return await _context.GeoLocationSearches.ToListAsync();
    }
}