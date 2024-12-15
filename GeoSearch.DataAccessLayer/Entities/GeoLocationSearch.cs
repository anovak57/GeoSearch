﻿namespace GeoSearch.DataAccessLayer.Entities;

public class GeoLocationSearch
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Radius { get; set; }
    public string Query { get; set; }
    
    public AppUser User { get; set; }
}