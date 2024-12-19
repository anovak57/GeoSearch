namespace GeoSearch.DataAccessLayer.Entities;

public class GeoLocationSearch
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Radius { get; set; }
    public string Query { get; set; }
    public List<GeoLocation> GeoLocations { get; set; } = new();
}