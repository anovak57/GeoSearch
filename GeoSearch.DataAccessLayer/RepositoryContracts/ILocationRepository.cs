using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.DataAccessLayer.RepositoryContracts;

public interface ILocationRepository
{
    Task<GeoLocationSearch> AddGeoLocationSearchWithLocations(GeoLocationSearch geoLocationSearch,
        List<GeoLocation> geoLocations);
    Task<IEnumerable<GeoLocation>> GetGeoLocations();
    Task<IEnumerable<GeoLocation>> GetGeoLocationsByCategory(string query);
    Task<IEnumerable<GeoLocationSearch>> GetLocationSearches();
    Task AddFavouriteLocation(int userId, int locationId);
    Task<IEnumerable<GeoLocation>> GetFavouriteLocations(int userId);
}