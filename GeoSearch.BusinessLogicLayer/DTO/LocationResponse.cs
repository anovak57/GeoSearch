namespace GeoSearch.BusinessLogicLayer.DTO;

public record LocationResponse(
    string? Name,
    double Latitude,
    double Longitude,
    string? Address,
    string? City,
    string? Region,
    string? PostalCode,
    List<string> Categories
    );