namespace GeoSearch.DataAccessLayer.Entities;

public class GeoLocationSearch
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Radius { get; set; }
    public string Query { get; set; }
}