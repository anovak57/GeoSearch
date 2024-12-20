using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.DataAccessLayer.RepositoryContracts;

public interface ILocationRepository
{
    Task<Dictionary<string, Category>> GetExistingCategoriesAsync(IEnumerable<string> categoryNames);
    Task AddCategoriesAsync(IEnumerable<Category> newCategories);
    Task<List<GeoLocation>> GetExistingGeoLocationsAsync();
    Task AddGeoLocationsAsync(IEnumerable<GeoLocation> newGeoLocations);
    Task AddGeoLocationSearchAsync(GeoLocationSearch geoLocationSearch);
    Task<IEnumerable<GeoLocation>> GetGeoLocationsAsync();
    Task<IEnumerable<GeoLocation>> GetGeoLocationsByCategoryAsync(string query);
    Task<IEnumerable<GeoLocationSearch>> GetLocationSearchesAsync();
    Task AddFavouriteLocationAsync(FavouriteLocation favouriteLocation);
    Task<bool> IsFavouriteLocationAsync(int userId, int locationId);
    Task<IEnumerable<GeoLocation>> GetFavouriteLocationsAsync(int userId);
    Task SaveChangesAsync();
}