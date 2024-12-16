namespace GeoSearch.DataAccessLayer.Entities;

public class FavouriteLocation
{
    public int Id { get; set; }
    public AppUser User { get; set; }
    public int UserId { get; set; }
    public GeoLocation Location { get; set; }
    public int LocationId { get; set; }
}