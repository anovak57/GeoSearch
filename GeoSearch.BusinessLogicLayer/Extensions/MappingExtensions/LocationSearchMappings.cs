using GeoSearch.BusinessLogicLayer.DTO;
using GeoSearch.DataAccessLayer.Entities;

namespace GeoSearch.BusinessLogicLayer.Extensions.MappingExtensions;

public static class LocationSearchMappings
{
    public static GeoLocationSearch MapToGeoLocationSearch(this LocationSearchRequest locationSearch)
    {
        return new GeoLocationSearch()
        {
            Latitude = locationSearch.Latitude,
            Longitude = locationSearch.Longitude,
            Query = locationSearch.Query,
            Radius = locationSearch.Radius
        };
    }
    
    public static LocationSearchResponse MapToLocationSearchResponse(this GeoLocationSearch locationSearch)
    {
        return new LocationSearchResponse(
            locationSearch.Latitude,
            locationSearch.Longitude,
            locationSearch.Query,
            locationSearch.Radius
        );
    }

    public static SearchResult MapToSearchResult(this GeoLocationSearch locationSearch)
    {
        return new SearchResult(
            locationSearch.Latitude,
            locationSearch.Longitude,
            locationSearch.Query,
            locationSearch.Radius,
            locationSearch.GeoLocations.Select(loc => loc.MapToLocationResponse())
        );
    }
}