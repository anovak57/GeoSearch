namespace GeoSearch.BusinessLogicLayer.DTO;

public record SearchResult(
    double Latitude,
    double Longitude,
    string Query,
    int Radius,
    IEnumerable<LocationResponse> Locations);