namespace GeoSearch.BusinessLogicLayer.DTO;

public record LocationSearchRequest(
    double Latitude,
    double Longitude,
    string Query = "",
    int Radius = 1000);
