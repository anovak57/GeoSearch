namespace GeoSearch.DataAccessLayer.Entities;

public class LocationCategory
{
    public int Id { get; set; }
    
    public int GeoLocationId { get; set; }
    public GeoLocation GeoLocation { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}