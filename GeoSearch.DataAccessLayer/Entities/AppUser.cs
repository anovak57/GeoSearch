using System.ComponentModel.DataAnnotations;

namespace GeoSearch.DataAccessLayer.Entities;

public class AppUser
{
    [Key]
    public int UserId { get; set; }
    public string Username { get; set; }
    public string ApiKey { get; set; }
    public List<FavouriteLocation> FavouriteLocations { get; set; } = new();
}