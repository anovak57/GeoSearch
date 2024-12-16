using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.DataAccessLayer.RepositoryContracts;

public interface ILocationRepository
{
    Task<IEnumerable<GeoLocation>> AddGeoLocations(IEnumerable<GeoLocation> locations);
    Task<GeoLocationSearch> AddLocationSearch(GeoLocationSearch locationSearch);
    Task<IEnumerable<GeoLocation>> GetGeoLocations();
    Task<IEnumerable<GeoLocationSearch>> GetLocationSearches();
}