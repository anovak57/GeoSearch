using System.ComponentModel.DataAnnotations;

namespace GeoSearch.DataAccessLayer.Entities;

public class AppUser
{
    [Key]
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string ApiKey { get; set; }
}