namespace GeoSearch.BusinessLogicLayer.DTO;

public class FoursquareResponse
{
    public List<FoursquarePlace> Results { get; set; }
}

public class FoursquarePlace
{
    public string Name { get; set; }
    public FoursquareGeocodes Geocodes { get; set; }
    public FoursquareLocation Location { get; set; }
    public List<FoursquareCategory> Categories { get; set; }
}

public class FoursquareGeocodes
{
    public FoursquareCoordinates Main { get; set; }
}

public class FoursquareCoordinates
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class FoursquareLocation
{
    public string Address { get; set; }
    public string Locality { get; set; }
    public string Region { get; set; }
    public string Postcode { get; set; }
}

public class FoursquareCategory
{
    public string Name { get; set; }
}