namespace GeoSearch.DataAccessLayer.Entities;

public class GeoLocation
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public List<LocationCategory> LocationCategories { get; set; } = new();
    public List<GeoLocationSearch> GeoLocationSearches { get; set; } = new();
}