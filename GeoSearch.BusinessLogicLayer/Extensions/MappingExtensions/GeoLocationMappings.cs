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
            location.Categories
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
            Categories = fsqPlace.Categories.Select(cat => cat.Name).ToList()
        };
    }
}