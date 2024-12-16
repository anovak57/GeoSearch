namespace GeoSearch.DataAccessLayer.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    
    public List<LocationCategory> LocationCategories { get; set; } = new();
}