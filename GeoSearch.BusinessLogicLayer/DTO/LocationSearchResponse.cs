namespace GeoSearch.BusinessLogicLayer.DTO;

public record LocationSearchResponse(
    double Latitude,
    double Longitude,
    string Query,
    int Radius);