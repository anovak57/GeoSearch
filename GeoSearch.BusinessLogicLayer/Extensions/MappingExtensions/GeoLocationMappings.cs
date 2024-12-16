using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.BusinessLogicLayer.Extensions.MappingExtensions;

public static class GeoLocationMappings
{
    public static LocationResponse ToLocationResponse(this GeoLocation location)
    {
        return new LocationResponse(
            location.Name,
            location.Latitude,
            location.Longitude,
            location.Address,
            location.City,
            location.Region,
            location.PostalCode,
            location.LocationCategories.Select(glc => glc.Category.Name).ToList()
        );
    }

    public static GeoLocation ToGeoLocation(this FoursquarePlace fsqPlace)
    {
        return new GeoLocation()
        {
            Name = fsqPlace.Name,
            Latitude = fsqPlace.Geocodes.Main.Latitude,
            Longitude = fsqPlace.Geocodes.Main.Longitude,
            Address = fsqPlace.Location.Address,
            City = fsqPlace.Location.Locality,
            Region = fsqPlace.Location.Region,
            PostalCode = fsqPlace.Location.Postcode,
            LocationCategories = fsqPlace.Categories.Select(cat => new LocationCategory
            {
                Category = new Category { Name = cat.Name }
            }).ToList()
        };
    }
    
    public static GeoLocation ToGeoLocation(this LocationResponse location)
    {
        return new GeoLocation()
        {
            Name = location.Name,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Address = location.Address,
            City = location.City,
            Region = location.Region,
            PostalCode = location.PostalCode,
            LocationCategories = location.Categories.Select(categoryName => new LocationCategory
            {
                Category = new Category { Name = categoryName }
            }).ToList()
        };
    }
}